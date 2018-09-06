using System;

namespace Sandbox.Domain
{
	public class ResourceNotFoundException : Exception
	{
		public ResourceNotFoundException() { }

		public ResourceNotFoundException(string message) 
			: base(message) { }

		public ResourceNotFoundException(string message, Exception innerException) 
			: base(message, innerException) { }

		public ResourceNotFoundException(Guid id, string domainModel) 
			: base(GetDefaultMessage(id, domainModel)) { }

		public ResourceNotFoundException(Guid id, string domainModel, Exception innerException) 
			: base(GetDefaultMessage(id, domainModel), innerException) { }

		private static string GetDefaultMessage(object id, string domainModel)
		{
			return $"{domainModel} not found with id {id}";
		}
	}
}
