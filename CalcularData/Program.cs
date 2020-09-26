using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace CalcularData
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string data;
                char operacao;
                long valor;

                // Pede ao usuario que digite a data
                Console.WriteLine("Digite a data e hora no formato dd/MM/yyyy HH:mm e pressione Enter");
                data = Convert.ToString(Console.ReadLine());

                // Pede ao usuario que digite a operação
                Console.WriteLine("Digite a operação: + ou -   e pressione Enter");
                operacao = Convert.ToChar(Console.ReadLine());

                // Pede ao usuario que digite os minutos
                Console.WriteLine("Digite os minutos e pressione Enter");
                valor = Convert.ToInt32(Console.ReadLine());

                int dia = Convert.ToInt32(data.Substring(0, 2));
                int mes = Convert.ToInt32(data.Substring(3, 2));
                int ano = Convert.ToInt32(data.Substring(6, 4));
                int horas = Convert.ToInt32(data.Substring(11, 2));
                int minutos = Convert.ToInt32(data.Substring(14, 2));

                // Calcula o resultado do método e mostra na tela
                FuncaoData dataCalculada = new FuncaoData();

                string resultado = dataCalculada.CalcularData(data, operacao, valor);
                Console.WriteLine("O resultado é " + resultado);
                Console.ReadLine();
            }
        }

    }
}
