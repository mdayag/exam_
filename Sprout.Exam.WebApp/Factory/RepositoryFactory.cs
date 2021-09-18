using System;

namespace Sprout.Exam.WebApp.Factory
{
    public static class RepositoryFactory
    {
        #region Repository Factory

        public static IRepository GetRepository(string repositoryTable, string repositoryType)
        {
            IRepository results = null;

            switch(repositoryTable)
            {
                case "Computers":
                    if (repositoryType == "SQL")
                    {
                        results = new RepositoryComputersSQL();
                    }
                    else if (repositoryType == "XML")
                    {
                        results = new RepositoryComputersXML();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Repository Type");
                    }
                    break;
                case "Users":
                    if (repositoryType == "SQL")
                    {
                        results = new RepositoryUsersSQL();
                    }
                    else if (repositoryType == "XML")
                    {
                        results = new RepositoryUsersXML();
                    }
                    else
                    {
                        throw new ArgumentException("Invalid Repository Type");
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid Repository Table");
            }

            return results;
        }

        #endregion Repository Factory

        #region Interfaces

        public interface IRepository
        {

        }

        public interface IRepositoryComputers : IRepository
        {
            void GetComputers();
        }

        public interface IRepositoryUsers : IRepository
        {
            void GetUsers();
        }

        #endregion Interfaces

        #region Repositories

        #region Computers

        public class RepositoryComputersSQL : IRepositoryComputers
        {
            public void GetComputers()
            {
                Console.WriteLine("Returning COMPUTERS SQL information...");
            }
        }

        public class RepositoryComputersXML : IRepositoryComputers
        {
            public void GetComputers()
            {
                Console.WriteLine("Returning COMPUTERS XML information...");
            }
        }

        #endregion Computers

        #region Users

        public class RepositoryUsersSQL : IRepositoryUsers
        {
            public void GetUsers()
            {
                Console.WriteLine("Returning USER SQL information...");
            }
        }

        public class RepositoryUsersXML : IRepositoryUsers
        {
            public void GetUsers()
            {
                Console.WriteLine("Returning USER XML information...");
            }
        }

        #endregion Users

        #endregion Repositories

        #region Test

        public static void TestRepositoryFactory()
        {
            // Computers
            var repoCpSql = (RepositoryComputersSQL)RepositoryFactory.GetRepository("Computers", "SQL");
            repoCpSql.GetComputers();

            IRepository repoCpXml = RepositoryFactory.GetRepository("Computers", "XML");
            dynamic cpXml = repoCpXml;
            cpXml.GetComputers();

            // Users
            var repoUsSql = RepositoryFactory.GetRepository("Users", "SQL");
            dynamic usSql = repoUsSql;
            usSql.GetUsers();

            IRepository repoUsXml = RepositoryFactory.GetRepository("Users", "XML");
            dynamic usXml = repoUsXml;
            usXml.GetUsers();

            Console.ReadKey();
        }

        #endregion Test
    }
}
