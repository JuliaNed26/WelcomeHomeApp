namespace WelcomeHome.Web;

public enum AuthorizationPolicies
{
    VerifiedVolunteerOnly,
    VolunteerOnly,
    ModeratorOnly,
    VerifiedVolunteerOrModerator,
}
