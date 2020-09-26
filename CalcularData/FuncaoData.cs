using System;

namespace CalcularData
{
    class FuncaoData : InterfaceData
    {
        public string dia { get; set; }
        public string mes { get; set; }
        public string ano { get; set; }
        public string hora { get; set; }
        public string minutos { get; set; }
        public string minutos_a_alterar { get; set; }
        public string operacao { get; set; }

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

        const int anoMinutos = 527040;     // ajuste para considerar 366 dias no ano para funcionar para os últimos 2 dias do ano
        const int diaMinutos = 1440;
        const int horaMinutos = 60;

        public string CalcularData(string data, char op, long valor)
        {
            dia = data.Split(new char[] { '/', ' ', ':' }, 5)[0];
            mes = data.Split(new char[] { '/', ' ', ':' }, 5)[1];
            ano = data.Split(new char[] { '/', ' ', ':' }, 5)[2];
            hora = data.Split(new char[] { '/', ' ', ':' }, 5)[3];
            minutos = data.Split(new char[] { '/', ' ', ':' }, 5)[4];

            operacao = op.ToString();
            valor = (valor < 0 ? valor * -1 : valor);

            minutos_a_alterar = valor.ToString();

            TotalMinutosConvertidopelaDataHora();

            // retorna em formato data e hora
            return String.Format("{0}/{1}/{2} {3}:{4}",
                calcula_dia_do_mes.ToString().PadLeft(2, '0'),
                calcula_mes.ToString().PadLeft(2, '0'),
                quociente_divisao_ano.ToString().PadLeft(4, '0'),
                quociente_divisao_hora.ToString().PadLeft(2, '0'),
                resto_divisao_hora.ToString().PadLeft(2, '0'));
        }

        private void TotalMinutosConvertidopelaDataHora()
        {
            ConverterAnoemMinutos();

            ConverterDiaemMinutos();

            ConverterDataHoraemMinutos();

            total_datahora_em_minutos = minutos_ano + minutos_dia + minutos_hora;

            ConverterMinutosemAno();
        }

        private void ConverterAnoemMinutos()
        {
            minutos_ano = Convert.ToInt32(ano) * anoMinutos;
        }

        private void ConverterDiaemMinutos()
        {
            int diaMes;

            switch (mes)
            {
                case "01":
                    diaMes = 0;
                    break;
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

        }

        private void ConverterDataHoraemMinutos()
        {
            minutos_hora = Convert.ToInt32(hora) * horaMinutos + Convert.ToInt32(minutos)
                + Convert.ToInt32(minutos_a_alterar) * (operacao == "+" ? 1 : -1);
        }

        private void ConverterMinutosemAno()
        {
            quociente_divisao_ano = total_datahora_em_minutos / anoMinutos;
            resto_divisao_ano = total_datahora_em_minutos % anoMinutos;
            ConverterMinutosemDia();
        }
        private void ConverterMinutosemDia()
        {
            quociente_divisao_dia = resto_divisao_ano / diaMinutos;
            resto_divisao_dia = resto_divisao_ano % diaMinutos;

            if (quociente_divisao_dia == 0) quociente_divisao_dia = 1;

            if (quociente_divisao_dia <= 31) { calcula_mes = 1; calcula_dia_do_mes = quociente_divisao_dia; }
            if (quociente_divisao_dia >= 32 && quociente_divisao_dia < 60) { calcula_mes = 2; calcula_dia_do_mes = quociente_divisao_dia - 32 + 1; }
            if (quociente_divisao_dia >= 60 && quociente_divisao_dia < 91) { calcula_mes = 3; calcula_dia_do_mes = quociente_divisao_dia - 60 + 1; }
            if (quociente_divisao_dia >= 91 && quociente_divisao_dia < 121) { calcula_mes = 4; calcula_dia_do_mes = quociente_divisao_dia - 91 + 1; }
            if (quociente_divisao_dia >= 121 && quociente_divisao_dia < 152) { calcula_mes = 5; calcula_dia_do_mes = quociente_divisao_dia - 121 + 1; }
            if (quociente_divisao_dia >= 152 && quociente_divisao_dia < 182) { calcula_mes = 6; calcula_dia_do_mes = quociente_divisao_dia - 152 + 1; }
            if (quociente_divisao_dia >= 182 && quociente_divisao_dia < 213) { calcula_mes = 7; calcula_dia_do_mes = quociente_divisao_dia - 182 + 1; }
            if (quociente_divisao_dia >= 213 && quociente_divisao_dia < 244) { calcula_mes = 8; calcula_dia_do_mes = quociente_divisao_dia - 213 + 1; }
            if (quociente_divisao_dia >= 244 && quociente_divisao_dia < 274) { calcula_mes = 9; calcula_dia_do_mes = quociente_divisao_dia - 244 + 1; }
            if (quociente_divisao_dia >= 274 && quociente_divisao_dia < 305) { calcula_mes = 10; calcula_dia_do_mes = quociente_divisao_dia - 274 + 1; }
            if (quociente_divisao_dia >= 305 && quociente_divisao_dia < 335) { calcula_mes = 11; calcula_dia_do_mes = quociente_divisao_dia - 305 + 1; }
            if (quociente_divisao_dia >= 335 && quociente_divisao_dia <= 365) { calcula_mes = 12; calcula_dia_do_mes = quociente_divisao_dia - 335 + 1; }

            ConverterMinutosemHora();
        }
        private void ConverterMinutosemHora()
        {
            quociente_divisao_hora = resto_divisao_dia / horaMinutos;
            resto_divisao_hora = resto_divisao_dia % horaMinutos;
        }
    }

}
