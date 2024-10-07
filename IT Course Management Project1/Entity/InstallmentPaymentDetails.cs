namespace IT_Course_Management_Project1.Entity
{
    public class InstallmentPaymentDetails
    {
        public int Id { get; set; }
        public string Nic { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal InstallmentAmount { get; set; }
        public string Installments { get; set; }
        public decimal PaymentDue { get; set; }
        public decimal PaymentPaid { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
