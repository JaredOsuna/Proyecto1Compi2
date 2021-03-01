﻿using System;
using System.Collections;
using System.Text;
using Proyecto1_Compiladores2.Modelos;
using Irony.Parsing;
using System.Windows.Forms;

namespace Proyecto1_Compiladores2.Analizador
{
    class SemanticoInterprete
    {
        public ArrayList consola;
        public ArrayList errores;
        private ArrayList subProgramas;
        private Entorno entornoGlobal;
        private Expresion retornoFuncion;
        private bool parar;
        private bool continuar;

        public SemanticoInterprete()
        {

        }

        public void iniciarAnalisisSintactico(ParseTreeNode root)
        {
            consola = new ArrayList();
            errores = new ArrayList();
            subProgramas = new ArrayList();
            entornoGlobal = new Entorno(null);
            retornoFuncion = null;
            parar = false;
            continuar = false;
            recorrer(root, entornoGlobal);
        }

        private string removerExtras(string token)
        {
            token = token.Replace(" (id)", "");
            token = token.Replace(" (Keyword)", "");
            token = token.Replace(" (Key symbol)", "");
            token = token.Replace(" (entero)", "");
            token = token.Replace(" (cadena)", "");
            token = token.Replace(" (real)", "");
            token = token.Replace(" (boleano)", "");

            return token;
        }

        private void ejecutarCase(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarIf(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarRepeat(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarWhile(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarFuncion(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarProcedimiento(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarLlamada(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarTipos(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarAsignacion(ParseTreeNode root, Entorno entorno)
        {

        }
        private void ejecutarFor(ParseTreeNode root, Entorno entorno)
        {

        }

        private Expresion resolverExpresion(ParseTreeNode root, Entorno ent)
        {
            switch (root.ToString())
            {
                case "EXPRESION":
                    if (root.ChildNodes.Count == 3)//Operador binario
                    {
                        if (root.ChildNodes[1].ToString().Contains("and"))
                        {
                            return operarAnd(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("="))
                        {
                            return operarIgual(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("<>"))
                        {
                            return operarDesigual(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains(">="))
                        {
                            return operarMayorIgual(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("<="))
                        {
                            return operarMenorIgual(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains(">"))
                        {
                            return operarMayor(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("<"))
                        {
                            return operarMenor(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("+"))
                        {
                            return operarSuma(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("-"))
                        {
                            return operarResta(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("*"))
                        {
                            return operarMultiplicacion(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("/"))
                        {
                            return operarDivision(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("%"))
                        {
                            return operarModulo(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                        else if (root.ChildNodes[1].ToString().Contains("or"))
                        {
                            return operarOr(resolverExpresion(root.ChildNodes[0], ent), resolverExpresion(root.ChildNodes[2], ent));
                        }
                    }
                    else if (root.ChildNodes.Count == 2)//Operador unario
                    {
                        if (root.ChildNodes[0].ToString().Contains("not"))
                        {
                            return operarNot(resolverExpresion(root.ChildNodes[1], ent));
                        }
                        else if (root.ChildNodes[0].ToString().Contains("-"))
                        {
                            return operarNegativo(resolverExpresion(root.ChildNodes[1], ent));
                        }
                    }
                    return resolverExpresion(root.ChildNodes[0], ent);
                case "ESTRUCTURA":
                    return resolverEstructura(root.ChildNodes[0], ent);
                case "LLAMADA":
                    return resolverLlamada(root.ChildNodes[0], ent);
                case "VARIABLE":
                    return buscarVariable(root.ChildNodes[0], ent);
                default:
                    if (root.ToString().Contains("cadena"))
                    {
                        return new Expresion(Simbolo.EnumTipo.cadena, root.ToString().Replace(" (cadena)", ""));
                    }
                    else if (root.ToString().Contains("entero"))
                    {
                        return new Expresion(Simbolo.EnumTipo.entero, root.ToString().Replace(" (entero)", ""));
                    }
                    else if (root.ToString().Contains("real"))
                    {
                        return new Expresion(Simbolo.EnumTipo.real, root.ToString().Replace(" (real)", ""));
                    }
                    else if (root.ToString().Contains("boleano"))
                    {
                        return new Expresion(Simbolo.EnumTipo.boleano, root.ToString().Replace(" (boleano)", ""));
                    }
                    break;
            }
            return new Expresion(Simbolo.EnumTipo.error, "ERROR");
        }
        private Expresion resolverEstructura(ParseTreeNode root, Entorno entorno)
        {
            return new Expresion(Simbolo.EnumTipo.error, "Error desconocido");
        }
        private Expresion operarNegativo(Expresion expresion1)
        {
            String exp1 = expresion1.valor.ToString();
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    return new Expresion(Simbolo.EnumTipo.real, 0 - double.Parse(expresion1.valor.ToString()));
                case Simbolo.EnumTipo.entero:
                    return new Expresion(Simbolo.EnumTipo.entero, 0 - int.Parse(expresion1.valor.ToString()));
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Negativo no definido para el tipo " + expresion1.tipo);
            }
        }
        private Expresion resolverLlamada(ParseTreeNode root, Entorno entorno)
        {
            return new Expresion(Simbolo.EnumTipo.error, "Error desconocido");
        }
        private Expresion buscarVariable(ParseTreeNode root, Entorno entorno)
        {
            return new Expresion(Simbolo.EnumTipo.error, "Error desconocido");
        }
        private Expresion operarSuma(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.cadena:
                    //Cadena (Entero || real || Caracter || boleano || Cadena)
                    return new Expresion(Simbolo.EnumTipo.cadena, expresion1.valor.ToString() + expresion2.valor.ToString());
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.cadena:
                            //real Cadena
                            return new Expresion(Simbolo.EnumTipo.cadena, expresion1.valor.ToString() + expresion2.valor.ToString());
                        case Simbolo.EnumTipo.entero:
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) + Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Suma no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.entero, int.Parse(expresion1.valor.ToString()) + int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.cadena:
                            //Entero Cadena
                            return new Expresion(Simbolo.EnumTipo.cadena, expresion1.valor.ToString() + expresion2.valor.ToString());
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) + Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Suma no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Suma no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarResta(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) - Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Resta no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.entero, int.Parse(expresion1.valor.ToString()) - int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) - Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Resta no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Resta no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarMultiplicacion(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) * Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Multiplicacion no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.entero, int.Parse(expresion1.valor.ToString()) * int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) * Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Multiplicacion no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Multiplicacion no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarDivision(Expresion expresion1, Expresion expresion2)
        {
            if (expresion2.valor.ToString().Equals("0") || expresion2.valor.ToString().Equals("0.0"))
            {
                return new Expresion(Simbolo.EnumTipo.error, "Division por 0 indefinida");
            }
            else
            {
                switch (expresion1.tipo)
                {
                    case Simbolo.EnumTipo.real:
                        switch (expresion2.tipo)
                        {
                            case Simbolo.EnumTipo.entero:
                            case Simbolo.EnumTipo.real:
                                //real Entero
                                return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) / Double.Parse(expresion2.valor.ToString()));
                            case Simbolo.EnumTipo.error:
                                return expresion2;
                            default:
                                return new Expresion(Simbolo.EnumTipo.error, "Division no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                        }
                    case Simbolo.EnumTipo.entero:
                        switch (expresion2.tipo)
                        {
                            case Simbolo.EnumTipo.entero:
                                //Entero Entero
                                return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) / Double.Parse(expresion2.valor.ToString()));
                            case Simbolo.EnumTipo.real:
                                //Entero real
                                return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) / Double.Parse(expresion2.valor.ToString()));
                            case Simbolo.EnumTipo.error:
                                return expresion2;
                            default:
                                return new Expresion(Simbolo.EnumTipo.error, "Division no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                        }
                    case Simbolo.EnumTipo.error:
                        return expresion1;
                    default:
                        return new Expresion(Simbolo.EnumTipo.error, "Division no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                }
            }
        }
        private Expresion operarModulo(Expresion expresion1, Expresion expresion2)
        {
            if (expresion2.valor.ToString().Equals("0") || expresion2.valor.ToString().Equals("0.0"))
            {
                return new Expresion(Simbolo.EnumTipo.error, "Modulo 0 indefinido");
            }
            else
            {
                switch (expresion1.tipo)
                {
                    case Simbolo.EnumTipo.real:
                        switch (expresion2.tipo)
                        {
                            case Simbolo.EnumTipo.entero:
                            case Simbolo.EnumTipo.real:
                                return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) % Double.Parse(expresion2.valor.ToString()));
                            case Simbolo.EnumTipo.error:
                                return expresion2;
                            default:
                                return new Expresion(Simbolo.EnumTipo.error, "Modulo no definido entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                        }
                    case Simbolo.EnumTipo.entero:
                        switch (expresion2.tipo)
                        {
                            case Simbolo.EnumTipo.entero:
                                //Entero Entero
                                return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) % Double.Parse(expresion2.valor.ToString()));
                            case Simbolo.EnumTipo.real:
                                //Entero real
                                return new Expresion(Simbolo.EnumTipo.real, Double.Parse(expresion1.valor.ToString()) % Double.Parse(expresion2.valor.ToString()));
                            case Simbolo.EnumTipo.error:
                                return expresion2;
                            default:
                                return new Expresion(Simbolo.EnumTipo.error, "Modulo no definido entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                        }
                    case Simbolo.EnumTipo.error:
                        return expresion1;
                    default:
                        return new Expresion(Simbolo.EnumTipo.error, "Division no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                }
            }
        }
        private Expresion operarAnd(Expresion expresion1, Expresion expresion2)
        {
            bool resultado1;
            bool resultado2;
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.boleano:
                    if (expresion1.valor.ToString().ToLower().Equals("true"))
                    {
                        resultado1 = true;
                    }
                    else
                    {
                        resultado1 = false;
                    }
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.boleano:
                            if (expresion2.valor.ToString().ToLower().Equals("true"))
                            {
                                resultado2 = true;
                            }
                            else
                            {
                                resultado2 = false;
                            }
                            return new Expresion(Simbolo.EnumTipo.boleano, resultado1 && resultado2);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion AND no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion AND no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarOr(Expresion expresion1, Expresion expresion2)
        {
            bool resultado1;
            bool resultado2;
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.boleano:
                    if (expresion1.valor.ToString().ToLower().Equals("true"))
                    {
                        resultado1 = true;
                    }
                    else
                    {
                        resultado1 = false;
                    }
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.boleano:
                            if (expresion2.valor.ToString().ToLower().Equals("true"))
                            {
                                resultado2 = true;
                            }
                            else
                            {
                                resultado2 = false;
                            }
                            return new Expresion(Simbolo.EnumTipo.boleano, resultado1 || resultado2);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion OR no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion OR no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarNot(Expresion expresion1)
        {
            bool resultado1;
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.boleano:
                    if (expresion1.valor.ToString().ToLower().Equals("true"))
                    {
                        resultado1 = true;
                    }
                    else
                    {
                        resultado1 = false;
                    }
                    return new Expresion(Simbolo.EnumTipo.boleano, !resultado1);
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion NOT no definida para el tipo " + expresion1.tipo);
            }
        }
        private Expresion operarMayor(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) > int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) > Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion mayor no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) > int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) > Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion mayor no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion mayor no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarMenor(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) < int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) < Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion menor no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) < int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) < Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion menor no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion menor no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarMayorIgual(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) >= int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) >= Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion mayor o igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) >= int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) >= Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion mayor o igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion mayor o igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarMenorIgual(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) <= int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) <= Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion menor o igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) <= int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) <= Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion menor o igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion menor o igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarIgual(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.boleano:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.boleano:
                            return new Expresion(Simbolo.EnumTipo.boleano, expresion1.valor.ToString().ToLower().Equals(expresion2.valor.ToString().ToLower()));
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.cadena:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.cadena:
                            return new Expresion(Simbolo.EnumTipo.boleano, expresion1.valor.ToString().Equals(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, false);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) == int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) == Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, false);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) == int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) == Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, false);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                case Simbolo.EnumTipo.nulo:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, true);
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.boleano, false);
                    }
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }
        private Expresion operarDesigual(Expresion expresion1, Expresion expresion2)
        {
            switch (expresion1.tipo)
            {
                case Simbolo.EnumTipo.boleano:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.boleano:
                            return new Expresion(Simbolo.EnumTipo.boleano, !expresion1.valor.ToString().ToLower().Equals(expresion2.valor.ToString().ToLower()));
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion igual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.cadena:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.cadena:
                            return new Expresion(Simbolo.EnumTipo.boleano, !expresion1.valor.ToString().Equals(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, true);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion desigual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.real:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) != int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //real Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, Double.Parse(expresion1.valor.ToString()) != Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, true);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion desigual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.entero:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.entero:
                            //Entero Entero
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) != int.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.real:
                            //Entero real
                            return new Expresion(Simbolo.EnumTipo.boleano, int.Parse(expresion1.valor.ToString()) != Double.Parse(expresion2.valor.ToString()));
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, true);
                        default:
                            return new Expresion(Simbolo.EnumTipo.error, "Operacion desigual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
                    }
                case Simbolo.EnumTipo.error:
                    return expresion1;
                case Simbolo.EnumTipo.nulo:
                    switch (expresion2.tipo)
                    {
                        case Simbolo.EnumTipo.nulo:
                            return new Expresion(Simbolo.EnumTipo.boleano, false);
                        case Simbolo.EnumTipo.error:
                            return expresion2;
                        default:
                            return new Expresion(Simbolo.EnumTipo.boleano, true);
                    }
                default:
                    return new Expresion(Simbolo.EnumTipo.error, "Operacion desigual no definida entre los tipos " + expresion1.tipo + " y " + expresion2.tipo);
            }
        }

        private void recorrer(ParseTreeNode root, Entorno entorno)
        {
            if (!parar && !continuar) //Comprueba si existe un break o continue
            {
                Expresion resultado;
                Entorno nuevoEntorno;
                ArrayList listaHijos;
                Simbolo simbolo = null;
                Expresion expresion;

                switch (root.ToString())
                {
                    case "CASE":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarCase(root, nuevoEntorno);
                        break;
                    case "IF":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarIf(root, nuevoEntorno);
                        break;
                    case "REPEAT":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarRepeat(root, nuevoEntorno);
                        break;
                    case "WHILE":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarWhile(root, nuevoEntorno);
                        break;
                    case "FUNCION":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarFuncion(root, nuevoEntorno);
                        break;
                    case "PROCEDIMIENTO":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarProcedimiento(root, nuevoEntorno);
                        break;
                    case "LLAMADA":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarLlamada(root, nuevoEntorno);
                        break;
                    case "Z_TIPOS":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarTipos(root, nuevoEntorno);
                        break;
                    case "ASIGNACION":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarAsignacion(root, nuevoEntorno);
                        break;
                    case "FOR":
                        nuevoEntorno = new Entorno(entorno);
                        ejecutarFor(root, nuevoEntorno);
                        break;
                    case "SUBPROGRAMA":
                    case "PROGRAMA":
                    case "SENTENCIA":
                    case "BEGIN_END":
                    case "Z_CONSTANTES":
                    case "Z_VARIABLES":
                    case "Z_DECLARACIONES":
                    case "Z_SUBPROGRAMAS":
                        foreach (ParseTreeNode hijo in root.ChildNodes)
                        {
                            recorrer(hijo, entorno);
                        }
                        break;

                    case "ABAJO":
                        break;
                    case "ARRIBA":
                        break;
                    case "FUNCION_HEAD":
                        break;
                    case "PROCEDIMIENTO_HEAD":
                        break;
                    case "OPCION_CASE":
                        break;
                    case "DECLARACION_CAMPOS_TYPE":
                        break;
                    case "D_CONSTANTE":
                        break;
                    case "D_VARIABLE":
                        if (root.ChildNodes.Count != 0)
                        {
                            if (root.ChildNodes[0].ToString().Equals("D_VARIABLE"))
                            {
                                foreach (ParseTreeNode hijo in root.ChildNodes)
                                {
                                    recorrer(hijo, entorno);
                                }
                            }
                            else
                            {
                                if (root.ChildNodes[1].ChildNodes[0].ToString().Contains("real"))
                                {
                                    simbolo = new Simbolo(Simbolo.EnumTipo.real, 0.0);
                                }
                                else if (root.ChildNodes[1].ChildNodes[0].ToString().Contains("boolean"))
                                {
                                    simbolo = new Simbolo(Simbolo.EnumTipo.boleano, false);
                                }
                                else if (root.ChildNodes[1].ChildNodes[0].ToString().Contains("integer"))
                                {
                                    simbolo = new Simbolo(Simbolo.EnumTipo.entero, 0);
                                }
                                else if (root.ChildNodes[1].ChildNodes[0].ToString().Contains("string"))
                                {
                                    simbolo = new Simbolo(Simbolo.EnumTipo.cadena, "");
                                }
                                if (root.ChildNodes.Count == 4)
                                {
                                    expresion = resolverExpresion(root.ChildNodes[3], entorno);
                                    if (expresion.tipo == Simbolo.EnumTipo.error)
                                    {
                                        MessageBox.Show(expresion.valor.ToString());
                                    }
                                    else
                                    {
                                        if (expresion.tipo != simbolo.tipo)
                                        {
                                            if (expresion.tipo == Simbolo.EnumTipo.entero && simbolo.tipo == Simbolo.EnumTipo.real)
                                            {
                                                simbolo.valor = expresion.valor;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Error de tipos");
                                                simbolo.tipo = Simbolo.EnumTipo.error;
                                            }
                                        }
                                        else
                                        {
                                            simbolo.valor = expresion.valor;
                                        }
                                    }
                                }
                                if (simbolo.tipo != Simbolo.EnumTipo.error)
                                {
                                    entorno.insertar(removerExtras(root.ChildNodes[0].ToString()), simbolo, root.ChildNodes[0].Token.Location.Line, root.ChildNodes[0].Token.Location.Column);
                                    MessageBox.Show("Variable: " + removerExtras(root.ChildNodes[0].ToString()) + "\nTipo: " + simbolo.tipo + "\nValor: " + simbolo.valor);
                                }
                            }
                        }
                        break;
                    case "ESTRUCTURA":
                        break;
                    case "CONTROLADOR":
                        break;
                    case "VALOR":
                        break;
                    case "OPERADOR":
                        break;
                    case "PA":
                        break;
                    case "PF":
                        break;
                    case "PFVL":
                        break;
                    case "PFVR":
                        break;
                    case "RANGO":
                        break;
                    case "R_ID":
                        break;
                    case "INDICE":
                        break;
                    case "T_DATO":
                        break;
                    case "T_ELEMENTAL":
                        break;
                    case "T_ESTRUCTURADO":
                        break;
                    case "T_ORDINAL":
                        break;
                    case "VARIABLE":
                        break;
                    default:
                        if (!root.ToString().Contains(" ("))
                            MessageBox.Show("Falto agregar " + root.ToString() + " al switch");
                        break;
                }
            }
        }
    }
}