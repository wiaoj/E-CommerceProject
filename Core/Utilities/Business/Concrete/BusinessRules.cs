using Core.Utilities.Results.Abstract;

#nullable enable

namespace Core.Utilities.Business.Concrete {
	public class BusinessRules : IBusinessRules {
		public IResult? Run(params IResult[] logics) {
			foreach(var logic in logics) {
				if(logic.IsSucceed.Equals(false))
					return logic;
			}
			return null;
		}
	}
}