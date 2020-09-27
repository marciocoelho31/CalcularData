using System;

namespace CalcularData
{
    class FuncaoData : InterfaceData
    {
        #region declarando variáveis públicas
        public string dia { get; set; }
        public string mes { get; set; }
        public string ano { get; set; }
        public string hora { get; set; }
        public string minutos { get; set; }
        public string minutos_a_alterar { get; set; }
        public string soma_ou_subtrai { get; set; }
        #endregion

        #region declarando variáveis estáticas
        static int minutos_ano { get; set; }
        static int minutos_dia { get; set; }
        static int minutos_hora { get; set; }
        static int total_datahora_em_minutos { get; set; }
        static int quociente_divisao_ano { get; set; }
        static int resto_divisao_ano { get; set; }
        static int quociente_divisao_dia { get; set; }
        static int resto_divisao_dia { get; set; }
        static int quociente_divisao_hora { get; set; }
        static int resto_divisao_hora { get; set; }
        static int calcula_dia_do_mes { get; set; }
        static int calcula_mes { get; set; }
        #endregion

        #region declarando constantes
        const int anoMinutos = 527040;      // considerando 366 dias para funcionar para os últimos dias do ano
        const int diaMinutos = 1440;        // minutos em um dia
        const int horaMinutos = 60;         // minutos em uma hora
        #endregion

        /// <summary>
        /// método desenvolvido por Marcio Coelho para efeito de avaliação
        /// github: https://github.com/marciocoelho31
        /// data: 27/09/2020
        /// </summary>
        /// <param name="data">Uma data em forma de String formatada no padrão "dd/MM/yyyy HH:mm"</param>
        /// <param name="operacao">Só poderá aceitar os caracteres ‘+’ e ‘-’</param>
        /// <param name="valor">Valor em minutos que deve ser acrescentado ou decrementado</param>
        /// <returns>
        /// Regras e Restrições:
        /// - Não é permitida a utilização de qualquer classe ou biblioteca não nativa
        /// - Não é permitida a utilização das classes DateTime e TimeSpan
        /// - Se o valor for menor que zero, o sinal deve ser ignorado(tratar como positivo)
        /// - Ignore o fato de fevereiro poder possuir 28 ou 29 dias. Considere-o sempre com 28
        /// - Ignore a existência de horário de verão
        /// 
        /// Exemplo de retorno: CalcularData("01/03/2010 23:00", '+', 4000) = "04/03/2010 17:40"
        /// </returns>
        public string CalcularData(string data, char operacao, long valor)
        {
            // valida a operação, só é permitido acrescentar ou decrementar
            if (operacao.ToString() != "+" && operacao.ToString() != "-")
            {
                return "Operação inválida";
            }

            // valida o tamanho do campo da data
            if (data.Length != 16)
            {
                return "Data inválida - digite a data e hora no formato dd/MM/yyyy HH:mm";
            }

            // captura o que foi passado como parâmetros
            dia = data.Split(new char[] { '/', ' ', ':' }, 5)[0];
            mes = data.Split(new char[] { '/', ' ', ':' }, 5)[1];
            ano = data.Split(new char[] { '/', ' ', ':' }, 5)[2];
            hora = data.Split(new char[] { '/', ' ', ':' }, 5)[3];
            minutos = data.Split(new char[] { '/', ' ', ':' }, 5)[4];

            soma_ou_subtrai = operacao.ToString();

            // Se o valor for menor que zero, o sinal deve ser ignorado (tratar como positivo)
            minutos_a_alterar = (valor < 0 ? valor * -1 : valor).ToString();

            // converte a data em minutos (valores inteiros)
            TotalMinutosConvertidopelaDataHora();

            // retorna em formato data e hora
            return String.Format("O resultado é {0}/{1}/{2} {3}:{4}",
                calcula_dia_do_mes.ToString().PadLeft(2, '0'),
                calcula_mes.ToString().PadLeft(2, '0'),
                quociente_divisao_ano.ToString().PadLeft(4, '0'),
                quociente_divisao_hora.ToString().PadLeft(2, '0'),
                resto_divisao_hora.ToString().PadLeft(2, '0'));
        }

        #region métodos de auxílio para cálculo da data
        private void TotalMinutosConvertidopelaDataHora()
        {
            ConverterAnoEmMinutos();

            ConverterDiaEmMinutos();

            ConverterHoraEmMinutos();

            total_datahora_em_minutos = minutos_ano + minutos_dia + minutos_hora
                + Convert.ToInt32(minutos_a_alterar) * (soma_ou_subtrai == "+" ? 1 : -1);

            ConverterMinutosEmAno();
        }

        private void ConverterAnoEmMinutos()
        {
            minutos_ano = Convert.ToInt32(ano) * anoMinutos;
        }

        private void ConverterDiaEmMinutos()
        {
            int diaMes;

            switch (mes)
            {
                case "02":
                    diaMes = 31;
                    break;
                case "03":
                    diaMes = 59;
                    break;
                case "04":
                    diaMes = 90;
                    break;
                case "05":
                    diaMes = 120;
                    break;
                case "06":
                    diaMes = 151;
                    break;
                case "07":
                    diaMes = 181;
                    break;
                case "08":
                    diaMes = 212;
                    break;
                case "09":
                    diaMes = 243;
                    break;
                case "10":
                    diaMes = 273;
                    break;
                case "11":
                    diaMes = 304;
                    break;
                case "12":
                    diaMes = 334;
                    break;
                default:
                    diaMes = 0;
                    break;
            }

            minutos_dia = diaMes * diaMinutos + Convert.ToInt32(dia) * diaMinutos;

            if (soma_ou_subtrai == "-")
            {
                // verificando quando o cálculo diminui o valor do ano para a nova data, para calcular corretamente o dia
                int minutos_dia_aux = diaMes * diaMinutos + Convert.ToInt32(dia) * diaMinutos - diaMinutos;
                int minutos_hora_aux = Convert.ToInt32(hora) * horaMinutos + Convert.ToInt32(minutos);
                int total_datahora_em_minutos_aux = minutos_ano + minutos_dia_aux + minutos_hora_aux + Convert.ToInt32(minutos_a_alterar) * -1;
                if (total_datahora_em_minutos_aux < minutos_ano)
                {
                    minutos_dia -= diaMinutos;
                }
            }
            else
            {
                // verificando quando o cálculo aumenta o valor do ano para a nova data, para calcular corretamente o dia
                int minutos_hora_aux = Convert.ToInt32(hora) * horaMinutos + Convert.ToInt32(minutos);
                int total_datahora_em_minutos_aux = minutos_ano + minutos_dia + minutos_hora_aux + Convert.ToInt32(minutos_a_alterar);
                if (total_datahora_em_minutos_aux > minutos_ano + anoMinutos)
                {
                    minutos_dia += diaMinutos;
                }
            }
        }

        private void ConverterHoraEmMinutos()
        {
            minutos_hora = Convert.ToInt32(hora) * horaMinutos + Convert.ToInt32(minutos);
        }

        private void ConverterMinutosEmAno()
        {
            quociente_divisao_ano = total_datahora_em_minutos / anoMinutos;
            resto_divisao_ano = total_datahora_em_minutos % anoMinutos;

            ConverterMinutosEmDia();
        }

        private void ConverterMinutosEmDia()
        {
            quociente_divisao_dia = resto_divisao_ano / diaMinutos;
            resto_divisao_dia = resto_divisao_ano % diaMinutos;

            if (quociente_divisao_dia == 0) quociente_divisao_dia = 1;

            #region calcula mês e dia do mês
            if (quociente_divisao_dia <= 31)
            {
                calcula_mes = 1;
                calcula_dia_do_mes = quociente_divisao_dia;
            }
            if (quociente_divisao_dia >= 32 && quociente_divisao_dia < 60)
            {
                calcula_mes = 2;
                calcula_dia_do_mes = quociente_divisao_dia - 32 + 1;
            }
            if (quociente_divisao_dia >= 60 && quociente_divisao_dia < 91)
            {
                calcula_mes = 3;
                calcula_dia_do_mes = quociente_divisao_dia - 60 + 1;
            }
            if (quociente_divisao_dia >= 91 && quociente_divisao_dia < 121)
            {
                calcula_mes = 4;
                calcula_dia_do_mes = quociente_divisao_dia - 91 + 1;
            }
            if (quociente_divisao_dia >= 121 && quociente_divisao_dia < 152)
            {
                calcula_mes = 5;
                calcula_dia_do_mes = quociente_divisao_dia - 121 + 1;
            }
            if (quociente_divisao_dia >= 152 && quociente_divisao_dia < 182)
            {
                calcula_mes = 6;
                calcula_dia_do_mes = quociente_divisao_dia - 152 + 1;
            }
            if (quociente_divisao_dia >= 182 && quociente_divisao_dia < 213)
            {
                calcula_mes = 7;
                calcula_dia_do_mes = quociente_divisao_dia - 182 + 1;
            }
            if (quociente_divisao_dia >= 213 && quociente_divisao_dia < 244)
            {
                calcula_mes = 8;
                calcula_dia_do_mes = quociente_divisao_dia - 213 + 1;
            }
            if (quociente_divisao_dia >= 244 && quociente_divisao_dia < 274)
            {
                calcula_mes = 9;
                calcula_dia_do_mes = quociente_divisao_dia - 244 + 1;
            }
            if (quociente_divisao_dia >= 274 && quociente_divisao_dia < 305)
            {
                calcula_mes = 10;
                calcula_dia_do_mes = quociente_divisao_dia - 274 + 1;
            }
            if (quociente_divisao_dia >= 305 && quociente_divisao_dia < 335)
            {
                calcula_mes = 11;
                calcula_dia_do_mes = quociente_divisao_dia - 305 + 1;
            }
            if (quociente_divisao_dia >= 335)
            {
                calcula_mes = 12;
                calcula_dia_do_mes = quociente_divisao_dia - 335 + 1;
            }
            #endregion 

            ConverterMinutosEmHora();
        }

        private void ConverterMinutosEmHora()
        {
            quociente_divisao_hora = resto_divisao_dia / horaMinutos;
            resto_divisao_hora = resto_divisao_dia % horaMinutos;


        }

        #endregion
    }

}
