using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProntuarioPsicologia
{
    public class HistoricoPago
    {
        public HistoricoPago(int id_pagamento, DateTime data_pagamento, float valor)
        {
            this.id_pagamento = id_pagamento;
            this.data_pagamento = data_pagamento;
            this.valor = valor;
        }

        public HistoricoPago() { } 

        public int id_pagamento {  get; set; }
        public DateTime data_pagamento { get; set; }
        public float valor {  get; set; }

        public static List<HistoricoPago> historicoPagos = new List<HistoricoPago>();

    }
}
