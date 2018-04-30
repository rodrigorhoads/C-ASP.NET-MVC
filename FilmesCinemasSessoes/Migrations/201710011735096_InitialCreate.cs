namespace FilmesCinemasSessoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        CinemaID = c.Int(nullable: false, identity: true),
                        SessaoID = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CinemaID);
            
            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        CinemaID = c.Int(nullable: false),
                        Rua = c.String(maxLength: 50),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        CEP = c.String(),
                    })
                .PrimaryKey(t => t.CinemaID)
                .ForeignKey("dbo.Cinemas", t => t.CinemaID)
                .Index(t => t.CinemaID);
            
            CreateTable(
                "dbo.Sessaos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CinemaID = c.Int(nullable: false),
                        FilmeID = c.Int(nullable: false),
                        Horario = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cinemas", t => t.CinemaID, cascadeDelete: true)
                .ForeignKey("dbo.Filmes", t => t.FilmeID, cascadeDelete: true)
                .Index(t => t.CinemaID)
                .Index(t => t.FilmeID);
            
            CreateTable(
                "dbo.Filmes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Genero = c.String(nullable: false, maxLength: 50),
                        ClassificacaoIndicativa = c.String(),
                        Duracao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessaos", "FilmeID", "dbo.Filmes");
            DropForeignKey("dbo.Sessaos", "CinemaID", "dbo.Cinemas");
            DropForeignKey("dbo.Enderecoes", "CinemaID", "dbo.Cinemas");
            DropIndex("dbo.Sessaos", new[] { "FilmeID" });
            DropIndex("dbo.Sessaos", new[] { "CinemaID" });
            DropIndex("dbo.Enderecoes", new[] { "CinemaID" });
            DropTable("dbo.Filmes");
            DropTable("dbo.Sessaos");
            DropTable("dbo.Enderecoes");
            DropTable("dbo.Cinemas");
        }
    }
}
