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

namespace ProyectoFinal
{
    public partial class Arbol : Form
    {

        class Nodo
        {
            public int dato;
            public Nodo izquierda;
            public Nodo derecha;
            public Nodo(int v, Nodo i, Nodo d)
            {
                dato = v;
                izquierda = i;
                derecha = d;
            }
        }

        private Nodo raiz = null;

        public Arbol()
        {
            InitializeComponent();
        }

        private void Pedir(string m, ref int x)
        {
            string userInput = Interaction.InputBox(m, "Input", "");
            if (!int.TryParse(userInput, out x))
            {
                MessageBox.Show("Ingrese un valor numérico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Crearnodo(ref Nodo q, int d)
        {
            q = new Nodo(d, q, q);
            q.izquierda = null;
            q.derecha = null;
        }

        private void Insertarnodo(ref Nodo r)
        {
            Nodo t1 = null, t2 = null, t = null;
            int resp = 1, d = 0;
            while (resp == 1)
            {
                if (r == null)
                {
                    Pedir("Ingrese el Dato para el Nodo Raiz", ref d);
                    Crearnodo(ref r, d);
                }
                else
                {
                    Pedir("\nIngrese el dato para el Nodo Hijo", ref d);
                    t1 = t2 = r;
                    while (t1 != null)
                    {
                        t2 = t1;
                        if (d < t2.dato)
                            t1 = t2.izquierda;
                        else
                            t1 = t2.derecha;
                    }
                    Crearnodo(ref t, d);
                    if (d < t2.dato)
                        t2.izquierda = t;
                    else
                        t2.derecha = t;
                }
                resp = (MessageBox.Show("Presione 'si' para seguir / 'No' para parar", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes) ? 1 : 0;
            }
        }
        private void Impinorden(Nodo t, TreeNodeCollection nodes)
        {
            if (nodes != null && t != null)
            {
                Impinorden(t.izquierda, nodes);
                nodes.Add(t.dato.ToString());
                Impinorden(t.derecha, nodes);
            }
        }

        private void BuscarNodo(Nodo r, int valorBuscado)
        {
            if (r == null)
            {
                MessageBox.Show("Nodo no encontrado");
            }
            else if (valorBuscado == r.dato)
            {
                MessageBox.Show($"Nodo encontrado: {r.dato}");
            }
            else if (valorBuscado < r.dato)
            {
                BuscarNodo(r.izquierda, valorBuscado);
            }
            else
            {
                BuscarNodo(r.derecha, valorBuscado);
            }
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            Insertarnodo(ref raiz);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (raiz == null) // si esta vacio el arbol , mandar msj de que no hay ningun nodo
            {
                MessageBox.Show("no hay nada que imprimir");
            }
            else
            {
                treeView1.Nodes.Clear();
                TreeNodeCollection nodes = treeView1.Nodes;
                Impinorden(raiz, nodes);
            }

           
        }
        private Nodo BuscarPadre(Nodo r, int valorBuscado)
        {
            Nodo padre = null;
            while (r != null && r.dato != valorBuscado)
            {
                padre = r;
                if (valorBuscado < r.dato)
                {
                    r = r.izquierda;
                }
                else
                {
                    r = r.derecha;
                }
            }
            return padre;
        }

        private void EliminarNodo(ref Nodo r, int valorBuscado)
        {
            Nodo padre = null;
            Nodo actual = r;

            while (actual != null && actual.dato != valorBuscado)
            {
                padre = actual;
                if (valorBuscado < actual.dato)
                {
                    actual = actual.izquierda;
                }
                else
                {
                    actual = actual.derecha;
                }
            }

            if (actual == null)
            {
                MessageBox.Show("Nodo no encontrado");
            }
            else
            {
                DialogResult resultado = MessageBox.Show($"¿Desea eliminar el nodo con valor {actual.dato}?", "Eliminar Nodo", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    EliminarNodoEncontrado(ref r, padre, actual);
                    MessageBox.Show("Nodo eliminado");
                }
                // Si el resultado es 'No', no hacemos nada.
            }
        }

        private void EliminarNodoEncontrado(ref Nodo r, Nodo padre, Nodo nodoAEliminar)
        {
            if (nodoAEliminar.derecha == null)
            {
                if (padre == null)
                {
                    r = nodoAEliminar.izquierda;
                }
                else
                {
                    if (nodoAEliminar.dato < padre.dato)
                    {
                        padre.izquierda = nodoAEliminar.izquierda;
                    }
                    else
                    {
                        padre.derecha = nodoAEliminar.izquierda;
                    }
                }
            }
            else if (nodoAEliminar.izquierda == null)
            {
                if (padre == null)
                {
                    r = nodoAEliminar.derecha;
                }
                else
                {
                    if (nodoAEliminar.dato < padre.dato)
                    {
                        padre.izquierda = nodoAEliminar.derecha;
                    }
                    else
                    {
                        padre.derecha = nodoAEliminar.derecha;
                    }
                }
            }
            else
            {
                Nodo sucesor = EncontrarSucesor(nodoAEliminar.derecha);
                nodoAEliminar.dato = sucesor.dato;
                EliminarNodoEncontrado(ref nodoAEliminar.derecha, null, sucesor);
            }
        }

        private Nodo EncontrarSucesor(Nodo nodo)
        {
            while (nodo.izquierda != null)
            {
                nodo = nodo.izquierda;
            }
            return nodo;
        }

        private void Arbol_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnbus_Click(object sender, EventArgs e)
        {
            int valorBuscado = 0;
            Pedir("Ingrese el valor que desea buscar", ref valorBuscado);
            BuscarNodo(raiz, valorBuscado);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int valorEliminar = 0;
            Pedir("Ingrese el valor que desea eliminar", ref valorEliminar);
            EliminarNodo(ref raiz, valorEliminar);

            // Después de eliminar, actualizamos la visualización del árbol
            treeView1.Nodes.Clear(); // Supongo que tu TreeView se llama treeView1
            Impinorden(raiz, treeView1.Nodes);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
