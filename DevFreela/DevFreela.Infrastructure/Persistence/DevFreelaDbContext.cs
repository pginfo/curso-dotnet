using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;


namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu projeto ASPNET Core1","Minha descrição de Projeto", 1, 1, 10000),
                new Project("Meu projeto ASPNET Core2","Minha descrição de Projeto", 1, 1, 20000),
                new Project("Meu projeto ASPNET Core3","Minha descrição de Projeto", 1, 1, 30000),
            };

            Users = new List<User>
            {
                new User("Luis Felipe","luisf@luisded.com.br", new DateTime(1992,1,1)),
                new User("Robert C Martin","robertm@luisded.com.br", new DateTime(1950,1,1)),
                new User("Anderson Soares","andersons@luisded.com.br", new DateTime(1980,1,1))
            };

            Skills = new List<Skill>
            {
                new Skill(".NET Core"),
                new Skill("C#"),
                new Skill("SQL")
            };
        }

        public List<Project> Projects { get; set;}
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> ProjectComments { get; set; }
    }
}
