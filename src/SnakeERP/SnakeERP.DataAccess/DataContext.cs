using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SnakeERP.Model;
using WalkingTec.Mvvm.Core;

namespace SnakeERP.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }

        /// <summary>
        /// 基础资料-省份
        /// </summary>
        public DbSet<BasicProvince> BasicProvinces { get; set; }

        /// <summary>
        /// 基础资料-市(区)
        /// </summary>
        public DbSet<BasicCity> BasicCitys { get; set; }

        /// <summary>
        /// 基础资料-市(区)
        /// </summary>
        public DbSet<BasicCounty> BasicCountys { get; set; }

        /// <summary>
        /// 基础资料-车辆类型
        /// </summary>
        public DbSet<BasicVehicleType> BasicVehicleTypes { get; set; }

        /// <summary>
        /// 基础资料-车辆品牌
        /// </summary>
        public DbSet<BasicVehicleBrand> BasicVehicleBrands { get; set; }

        /// <summary>
        /// 基础资料-车辆信息
        /// </summary>
        public DbSet<BasicVehicleInfo> BasicVehicleInfos { get; set; }

        /// <summary>
        /// 基础资料-物流信息
        /// </summary>
        public DbSet<BasicLogistics> BasicLogisticss { get; set; }

        /// <summary>
        /// 基础资料-物流站点
        /// </summary>
        public DbSet<BasicLogisticsPosition> BasicLogisticsPositions { get; set; }

        /// <summary>
        /// 基础资料-客户
        /// </summary>
        public DbSet<BasicCustomer> BasicCustomers { get; set; }

        /// <summary>
        /// 发货-订单信息
        /// </summary>
        public DbSet<DGOrderInfo> DGOrderInfos { get; set; }

        /// <summary>
        /// 发货-订单明细
        /// </summary>
        public DbSet<DGOrderDetail> DGOrderDetails { get; set; }

        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }

        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };
                
                var adminmenus = Set<FrameworkMenu>().Where(x => x.Url != null && x.Url.StartsWith("/api") == false).ToList();
                foreach (var item in adminmenus)
                {
                    item.Url = "/_admin" + item.Url;
                }

                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();
            }
            return state;
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("your full connection string", DBTypeEnum.SqlServer);
        }
    }

}
