using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.domain.Interfaces
{
    public interface IState
    {
        bool Delete (int ID);
        IEnumerable<State> List ();
        State Get (int ID);
        State Add (State state);
        State Update (int ID, State state);
    }
}
