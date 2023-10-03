using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XyloCode.ThirdPartyServices.Cdek.Models
{
    public class CashOnDeliveryRegister
    {

        /// <summary>
        /// Номер реестра наложенного платежа
        /// </summary>
        public int RegistryNumber { get; set; }

        /// <summary>
        /// Фактическая дата оплаты реестра наложенного платежа
        /// </summary>
        public DateOnly PaymentDate { get; set; }

        /// <summary>
        /// Сумма по реестру (в валюте взаиморасчетов)
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Номер платежного поручения, в рамках которого был осуществлен платеж
        /// Если атрибут отсутствует в ответе или пустой, то для уточнения причин, пожалуйста, свяжитесь со своим менеджером
        /// </summary>
        public string PaymentOrderNumber { get; set; }

        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderPayment> Orders { get; set; }
    }


    public class OrderPayment
    {
        /// <summary>
        /// Номер заказа в ИС СДЭК
        /// </summary>
        public string CdekNumber { get; set; }

        /// <summary>
        /// Сумма к начислению (в валюте взаиморасчетов)
        /// </summary>
        public decimal TransferSum { get; set; }

        /// <summary>
        /// Сумма наложенного платежа, которую взяли с получателя (в валюте взаиморасчетов)
        /// </summary>
        public decimal PaymentSum { get; set; }

        /// <summary>
        /// Итоговая стоимость заказа без учета агентского вознаграждения  (в валюте взаиморасчетов)
        /// </summary>
        public decimal TotalSumWithoutAgent { get; set; }

        /// <summary>
        /// Агентское вознаграждение по переводу наложенного платежа (в валюте взаиморасчетов)
        /// </summary>
        public decimal AgentCommissionSum { get; set; }
    }


}
