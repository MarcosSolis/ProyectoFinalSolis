using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoFinal
{
    public partial class Grafos : Form
    {
        private int V; // Número de vértices
        private List<int>[] listaAdyacencia; // Lista de adyacencia

        public Grafos()
        {
            InitializeComponent();
        }

        private void AgregarArista(int origen, int destino)
        {
            listaAdyacencia[origen].Add(destino);
        }

        private void ImprimirGrafo()
        {
            
            // Borrar el contenido actual antes de imprimir
            textBox1.Text = "Representación del Grafo Dirigido:" + Environment.NewLine;

            for (int i = 0; i < V; i++)
            {
                textBox1.Text += $"Vértice {i} -> ";
                foreach (var nodo in listaAdyacencia[i])
                {
                    textBox1.Text += $"{nodo} ";
                }
                textBox1.Text += Environment.NewLine;
            }
        }

        private void CrearGrafo()
        {
            if (int.TryParse(textBoxVertices.Text, out V) && V > 0)
            {
                listaAdyacencia = new List<int>[V];
                for (int i = 0; i < V; i++)
                {
                    listaAdyacencia[i] = new List<int>();
                }

                MessageBox.Show($"Grafo creado con éxito. Tamaño del grafo: {V}", "Éxito");
                textBoxVertices.Text = "";
            }
            else
            {
                MessageBox.Show("Ingrese un número válido de vértices.", "Error");
            }
        }

        private bool ExisteNodo(int valor)
        {
            // Recorrer todos los vértices
            for (int i = 0; i < V; i++)
            {
                // Buscar el valor en la lista de adyacencia del vértice actual
                if (listaAdyacencia[i].Contains(valor))
                {
                    return true; // El nodo existe
                }
            }
            return false; // El nodo no existe
        }

        private void EliminarNodo(int valor)
        {
            // Recorrer todos los vértices
            for (int i = 0; i < V; i++)
            {
                // Eliminar el valor de la lista de adyacencia del vértice actual
                listaAdyacencia[i].Remove(valor);
            }
        }

        private void BuscarYEliminarNodo()
        {
            if (int.TryParse(textBoxBuscarNodo.Text, out int valor))
            {
                if (ExisteNodo(valor))
                {
                    DialogResult resultado = MessageBox.Show($"¿Desea eliminar el nodo {valor}?", "Eliminar Nodo", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        EliminarNodo(valor);
                        MessageBox.Show($"Nodo {valor} eliminado con exito.", "Éxito");
                        textBoxBuscarNodo.Text = "";
                        ImprimirGrafo(); // Actualizar la representación del grafo después de la eliminación
                    }
                }
                else
                {
                    MessageBox.Show($"Nodo {valor} no encontrado.", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("Ingrese un valor numérico válido para buscar el nodo.", "Error");
            }
        }





        private void Grafos_Load(object sender, EventArgs e)
        {

        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            CrearGrafo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Obtén los valores de origen y destino desde los TextBoxes
            if (int.TryParse(textBoxOrigen.Text, out int origen) && int.TryParse(textBoxDestino.Text, out int destino))
            {
                // Verifica si los vértices están en el rango permitido
                if (origen >= 0 && origen < V && destino >= 0 && destino < V)
                {
                    AgregarArista(origen, destino);
                    MessageBox.Show($"Arista {origen} -> {destino} creada con éxito.", "Éxito");
                    textBoxDestino.Text = "";
                    textBoxOrigen.Text = "";
                }
                else
                {
                    MessageBox.Show("Los vértices deben estar en el rango permitido.", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("Ingrese valores válidos para origen y destino.", "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listaAdyacencia == null)
            {
                MessageBox.Show("Grafo vacio , no hay nada que imprimir");

            }
            else
            {
                ImprimirGrafo();
            }

           
        }

        private void btnbus_Click(object sender, EventArgs e)
        {
            BuscarYEliminarNodo();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
