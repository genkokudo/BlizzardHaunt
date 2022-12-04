using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    // BlizzardHaunt.ServerのApplicationDbContextと共有しようかと思ったけど、
    // アクセスできるテーブルは関数ごとに限定すべき（本体全部アクセスは不適）だと思ったので
    // Functions用のコンテキストを作成

    public class ApplicationDbContext : DbContext
    {
        //public DbSet<General> Generals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
    }

    // ※ここからDBアクセスしても良いけど、外部ソリューションを共有プロジェクトとしたらGithubActionsで失敗するので、こっちのプロジェクトにもBostNexと同じEntityクラスを作ること
}
