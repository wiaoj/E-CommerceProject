namespace Core.Utilities.Messages {
	internal static class WriteRepositoryBaseMessages {
		public static (String Successful, String Unsuccessful) AddedEntity =>
			("Adding operation to database completed successfully.",
			"Insertion operation to the database was failed.");

		public static (String Successful, String Unsuccessful) RemoveEntity =>
			("Deletion operation from database completed successfully.",
			"Deletion operation from database was failed.");

		public static (String Successful, String Unsuccessful) UpdateEntity =>
			("Update operation to the database completed successfully.",
			"Update operation to the database was failed.");

		public static (String Information, String Warning) SaveDatabase =>
			("The update operation on the database completed successfully.",
			"The update operation on the database was failed.");

	}
}