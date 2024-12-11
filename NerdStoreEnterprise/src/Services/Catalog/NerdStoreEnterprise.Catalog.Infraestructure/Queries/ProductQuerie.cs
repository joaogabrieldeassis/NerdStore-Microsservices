namespace NerdStoreEnterprise.Catalog.Infraestructure.Queries;

public static class ProductQuerie
{
    public static string GetAll()
       => $@"SELECT * FROM NerdStore.dbo.Produtos";

    public static string GetById()
       => $@"SELECT * FROM NerdStore.dbo.Produtos AS p WHERE p.Id = @Id";
}