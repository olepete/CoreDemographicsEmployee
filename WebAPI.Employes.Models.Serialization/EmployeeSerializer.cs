using System;
using System.Collections.Generic;
using MongoDB.Driver;
using WebAPI.Employees.Models.Domain;

namespace WebAPI.Employes.Models.Serialization
{
	public class EmployeeSerializer : IEmployeeSerializer
	{
		public EmployeeSerializer(string mongoConnection, string databaseName, string collectionName)
		{
			var client = new MongoClient(mongoConnection);
			_db = client.GetDatabase(databaseName);
			this.collectionName = collectionName;
		}

		public InternalEmployee AddEmployee(InternalEmployee model)
		{
			//in case we eventually need to get defaulted data from a db, we don't yet
			Employees.InsertOne(model);
			return model;
		}

		public void DeleteEmployee(string id)
		{
			var item = Employees.Find(e => e.Id == id).FirstOrDefault();
            //only update if it isn't deleted already
			if (item.IsActive)
			{
				item.IsActive = false;
				if (item.ActionLogs == null)
					item.ActionLogs = new List<ActionLog>();
				item.ActionLogs.Add(new ActionLog() { ActionDate = DateTime.UtcNow, ActionDescription = "Deleted record" });

				Employees.ReplaceOne(u => u.Id == id, item);
			}
		}

		public InternalEmployee GetEmployee(string id)
		{
			return Employees.Find(e => e.Id == id).FirstOrDefault();
		}

		public List<InternalEmployee> GetEmployees()
		{
			return Employees.Find(e => e.IsActive == true).ToList();
		}

		public void UpdateEmployee(string id, InternalEmployee model)
		{
			Employees.ReplaceOne(e => e.Id == id, model);
		}

        public bool EmployeeExists( string id )
		{
			var employee = Employees.Find(e => e.Id == id).FirstOrDefault();
			return employee != null;
		}

		public IMongoCollection<InternalEmployee> Employees { get { return _db.GetCollection<InternalEmployee>(collectionName); } }
        private IMongoDatabase _db;
		private string collectionName;
        

	}
}
