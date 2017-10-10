public enum SocialNetworkType
{
    Facebook,
    Twitter
}

/// <summary>
/// Need to use SocialNetworkType for work with certain social network.
/// </summary>
public interface ISocial
{

    void Login();

}
