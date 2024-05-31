namespace Clones.EducationLogic
{
    public abstract class EducationHandler
    {
        public EducationHandler Successor { get; set; }
        public abstract void Handle();
    }
}
