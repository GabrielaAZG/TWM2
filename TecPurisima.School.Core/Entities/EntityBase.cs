namespace TecPurisima.School.Core.Entities;

public class EntityBase: EntityBaseWithoutAudit
{
    //public int CreatedById { get; set; } 
    //public Admin CreatedBy { get; set; }
    public string CreatedBy { get; set; }
    //public int UpdatedById { get; set; }
    //public Admin UpdatedBy { get; set; }
    public string UpdatedBy { get; set; }
}


public class Test1 : EntityBase
{
    
    
    
}