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
            // rotina fica em loop até o usuário der Control C para abortar
            while (true)
            {
                string data;
                char operacao;
                long valor;

                Console.Clear();

                // Pede ao usuario que digite a data
                Console.WriteLine("Digite a data e hora no formato dd/MM/yyyy HH:mm e pressione Enter,\nou digite Control C a qualquer momento para abortar a operação");
                data = Convert.ToString(Console.ReadLine());

                // Pede ao usuario que digite a operação
                Console.WriteLine("Digite a operação: + ou -   e pressione Enter");
                operacao = Convert.ToChar(Console.ReadLine());

                // Pede ao usuario que digite os minutos
                Console.WriteLine("Digite os minutos e pressione Enter");
                valor = Convert.ToInt32(Console.ReadLine());

                // Calcula o resultado do método e mostra na tela
                FuncaoData dataCalculada = new FuncaoData();

                string resultado = dataCalculada.CalcularData(data, operacao, valor);
                Console.WriteLine("\n\nO resultado é " + resultado);
                Console.ReadLine();
            }
        }

    }
}
