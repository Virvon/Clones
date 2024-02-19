namespace Clones.EducationLogic
{
    public abstract class EducationHandler
    {
        public EducationHandler Successor { get; set; }
        public abstract void Handle();
    }

    public class ShowQuestHandler : EducationHandler
    {
        public override void Handle()
        {
            throw new System.NotImplementedException();
        }
    }
}
