using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ContactsAppModel
{
	/// <summary>
	/// Класс <see cref="ProjectManager"/> предназначен
	/// для организации сериализации и десериализации
	/// объектов класса <see cref="Project">
	/// </summary>
	public static class ProjectManager
	{
		/// <summary>
		/// Метод выполныет десериализацию объекта
		/// </summary>
		/// <returns>Десериализованный объект</returns>
		public static Project ReadProject(FileInfo path)
		{
			if (!path.Directory.Exists)
			{
				path.Directory.Create();
			}
			if (!File.Exists(path.FullName))
			{
				path.Create().Close();
			}

			Project project = new Project();

			using (StreamReader file = new StreamReader(
					Convert.ToString(path), Encoding.Default))
			{
				string projectContent = file.ReadLine();
				if (string.IsNullOrEmpty(projectContent))
				{
					return project;
				}

				project = JsonConvert.DeserializeObject<Project>(projectContent);
			}

			return project;
		}

		/// <summary>
		/// Метод выполняет сериализацию объекта
		/// </summary>
		/// <param name="project">Сериализуемый объект</param>
		public static void SaveProject(Project project,
			FileInfo path)
		{
			if (!path.Directory.Exists)
			{
				path.Directory.Create();
			}
			if (!File.Exists(path.FullName))
			{
				path.Create().Close();
			}

			using (StreamWriter file = new StreamWriter(
				Convert.ToString(path), false, Encoding.UTF8))
			{
				file.Write(JsonConvert.SerializeObject(project));
			}
		}
	}
}
