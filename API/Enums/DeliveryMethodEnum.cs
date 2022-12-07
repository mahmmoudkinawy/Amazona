namespace API.Enums;
public enum OrderStatusEnum
{
    [EnumMember(Value = "Pending")]
    Pending,

    [EnumMember(Value = "Payment Recevied")]
    PaymentRecevied,

    [EnumMember(Value = "Payment Failed")]
    PaymentFailed
}
