using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Enums
{
    public enum CartStatus
    {
        [Display(Name = "لغوشده")]
        Cancellation,
        [Display(Name = "پرداخت نشده")]
        Unpaid,
        [Display(Name = "درحال پرداخت")]
        PendingPayment,
        [Display(Name = "پرداخت شده")]
        Paid
    }
}
