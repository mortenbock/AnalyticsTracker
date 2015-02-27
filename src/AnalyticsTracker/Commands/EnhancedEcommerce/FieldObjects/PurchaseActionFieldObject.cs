namespace Vertica.AnalyticsTracker.Commands.EnhancedEcommerce.FieldObjects
{
	public class PurchaseActionFieldObject : ActionFieldObject
	{
		public PurchaseActionFieldObject(string id, string affiliation, decimal revenue, decimal tax, decimal shipping, string coupon)
			: base(id, affiliation, revenue, tax, shipping, coupon, null, null, null)
		{ }
	}
}