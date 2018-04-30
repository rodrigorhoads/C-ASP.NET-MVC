namespace FilmesCinemasSessoes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CEPMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enderecoes", "Rua", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Enderecoes", "CEP", c => c.String(nullable: false));
            AlterColumn("dbo.Filmes", "Duracao", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Filmes", "Duracao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Enderecoes", "CEP", c => c.String());
            AlterColumn("dbo.Enderecoes", "Rua", c => c.String(maxLength: 50));
        }
    }
}
