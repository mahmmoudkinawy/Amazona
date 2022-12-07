namespace API.Specifications;
public sealed class OrderWithItemsAndOrderingSpecification : BaseSpecification<OrderEntity>
{
    public OrderWithItemsAndOrderingSpecification(string email)
        : base(_ => _.BuyerEmail == email)
    {
        AddInclude(_ => _.DeliveryMethod);
        AddInclude(_ => _.OrderItems);
        AddOrderByDesc(_ => _.OrderDate);
    }

    public OrderWithItemsAndOrderingSpecification(int id, string email)
        : base(_ => _.BuyerEmail == email && _.Id == id)
    {
        AddInclude(_ => _.DeliveryMethod);
        AddInclude(_ => _.OrderItems);
    }
}
