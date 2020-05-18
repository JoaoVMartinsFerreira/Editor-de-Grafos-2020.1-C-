using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
namespace Editor_de_Grafos
{
    public class Grafo : GrafoBase, iGrafo
    {
        private bool[] visitado = new bool[100];
        //private int[] vetAgm;
        //private Color cor;

        public void limparVertice(int n)
        {
            for (int i = 0; i < getN(); i++)
            {
                getVertice(i).setCor(Color.Blue);
            }
        }

        public void limparAresta(int n)
        {
            for (int i = 1; i < matAdj.GetLength(0); i++)
            {
                for (int j = 0; j < matAdj.GetLength(1); j++)
                {
                    if (matAdj[i, j] != null)
                    {
                        getAresta(i, j).setCor(Color.Black);   
                    }
                }
            }
        }
        public void AGM(int v) // AGM -----------------------------------------------------------------------------------------------------------------------------------------------------------------
        {

            visitado = new bool[getN() + 1];
            Fila f = new Fila(matAdj.GetLength(0));
            f.enfileirar(v);
            visitado[v] = true;
            int soma = 0;
            int k = 0;
            limparAresta(v);
            while (!f.vazia())
            {
                v = f.desenfileirar();
                for (; k < matAdj.GetLength(0); k++)
                {
                    for (int r = 1; r < matAdj.GetLength(1); r++)
                    {

                        for (int l = 0; l < matAdj.GetLength(0); l++)
                        {
                            if (matAdj[k, l] != null && matAdj[k, r] != null && matAdj[k, l].getPeso() < matAdj[k, r].getPeso() && !visitado[k])
                            {

                                soma += matAdj[k, l].getPeso();
                                Thread.Sleep(1000);
                                getAresta(k, l).setCor(Color.Red);
                                visitado[k] = true;
                                f.enfileirar(k);
                            }

                        }

                    }
                }
            }
        }

        public void coloracaoVertice(int v)// Coloração de vértices -------------------------------------------------------------------------------------------------------------------------------
        {

            List<Color> listaCores = new List<Color>();
            listaCores.Add(Color.Red);
            listaCores.Add(Color.MediumSeaGreen);
            listaCores.Add(Color.Gray);
            listaCores.Add(Color.Pink);
            listaCores.Add(Color.Olive);
            listaCores.Add(Color.Violet);
            listaCores.Add(Color.Orange);

            List<Color> newCores = new List<Color>();
            newCores.Add(Color.Red);
            visitado = new bool[getN()];
            Fila f = new Fila(matAdj.GetLength(0));
            f.enfileirar(v);
            visitado[v] = true;
            getVertice(v).setCor(listaCores[0]);

            int contaCores = 0;
            limparVertice(v);
            while (!f.vazia())
            {
                v = f.desenfileirar();
                for (int i = 0; i < matAdj.GetLength(0); i++)
                {
                    if (matAdj[v, i] != null && !visitado[i])
                    {
                        if (getVertice(i - 1) == getVertice(0))
                        {
                            Thread.Sleep(1000);
                            getVertice(i).setCor(listaCores[i]);
                            newCores.Add(listaCores[i]);
                            visitado[i] = true;
                            f.enfileirar(i);


                            for (int j = 2; j < getN(); j++)
                            {
                                for (int k = 0; k < listaCores.Count; k++)
                                {
                                    if (getVertice(j - 1).getCor() != listaCores[k])
                                    {
                                        Thread.Sleep(1000);
                                        getVertice(j).setCor(listaCores[k]);
                                        visitado[i] = true;
                                        f.enfileirar(i);
                                        newCores.Add(listaCores[k]);
                                        contaCores = k;
                                        //break;
                                    }



                                    //if (!getAdjacentes(j).Contains(getVertice(k)))
                                    //{
                                    //    Thread.Sleep(1000);
                                    //    getVertice(k).setCor(getVertice(j).getCor());
                                    //    visitado[k] = true;
                                    //    f.enfileirar(k);
                                    //    //newCores.Add(listaCores[j]);

                                    //}
                                    //else
                                    //{
                                    //    Thread.Sleep(1000);
                                    //    getVertice(k).setCor(listaCores[j]);
                                    //    visitado[k] = true;
                                    //    f.enfileirar(k);
                                    //}
                                }

                            }
                        }
                    }
                }
            }
        }

        public void caminhoMinimo(int i, int j) // CAMINHO MINIMO -------------------------------------------------------------------------------------------------------------------------------
        {

        }

        public void completarGrafo() // COMPLETAR GRAFO ---------------------------------------------------------------------------------------------------------------------------------------------
        {

            Random rdn = new Random();

            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (getAresta(i, j) == null)
                    {
                        setAresta(i, j, rdn.Next(0, 30));
                    }
                }
            }
        }

        public bool isEuleriano() // EULERIANO ---------------------------------------------------------------------------------------------------------------------------------------------------------------
        {
            int i;
            for (i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                    return false;

            }
            if (getN() != 0)
                return true;
            else
                return false;
        }

        public bool isUnicursal() // UNICURSAL // ----------------------------------------------------------------------------------------------------------------------------------------------------------------
        {
            int i, cont = 0;
            for (i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                {
                    cont++;
                }
                if (cont == 2)
                {
                    return true;
                }
            }
            if (getN() != 0)
                return false;

            return false;
        }

        public void largura(int v) // LARGURA ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        {
            
            visitado = new bool[getN()];
            Fila f = new Fila(matAdj.GetLength(0));
            f.enfileirar(v);
            visitado[v] = true;
            limparAresta(v);

            while (!f.vazia())
            {
                v = f.desenfileirar();
                for (int i = 0; i < matAdj.GetLength(0); i++)
                {
                    if (matAdj[v, i] != null && !visitado[i])
                    {
                        Thread.Sleep(1000);
                        getAresta(v, i).setCor(Color.Red);
                        visitado[i] = true;
                        f.enfileirar(i);
                    }
                }
            }
        }

        public void numeroCromatico()
        {
        }

        public String paresOrdenados() // PARES ORDENADOS --------------------------------------------------------------------------------------------------------------------------------------------
        {
            string msg = "E={";

            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (getAresta(i, j) != null)
                    {
                        msg += "(" + i + "," + j + "),";
                    }
                    msg = msg.Substring(0, msg.Length - 1) + "}";
                }
            }
            return msg;

        }

        public void profundidade(int v) // PROFUNDIDADE -----------------------------------------------------------------------------------------------------------------------------------------------------
        {
            //visitado = new bool[getN()];
            visitado[v] = true;
           
            for (int i = 0; i < matAdj.GetLength(0); i++)
            {
                if (matAdj[v, i] != null && !visitado[i])
                {

                    Thread.Sleep(1000);
                    getAresta(v, i).setCor(Color.Red);
                    visitado[i] = true;
                    profundidade(i);
                }
            }
        }


    }
}
