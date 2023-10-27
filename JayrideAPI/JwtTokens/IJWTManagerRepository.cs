using JayrideModel;

namespace JayrideAPI.JwtTokens
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(LoginUserDBModel users);
    }
}
