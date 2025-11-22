// Reference: https://github.com/TobiasBuchholz/Plugin.Firebase/blob/master/sample/Playground/Common/Services/Auth/AuthService.cs

namespace CustomerFrontend.Services;

// TODO: Implement Windows?

#if ANDROID || IOS
using Plugin.Firebase.Auth;

public sealed class AuthService(IFirebaseAuth firebaseAuth) {
    public IFirebaseUser CurrentUser {
        get => firebaseAuth.CurrentUser;
    }
    
    public async Task<IFirebaseUser> SignIn(string email, string password) {
        try {
            var user = await firebaseAuth.SignInWithEmailAndPasswordAsync(email, password);
            return user;
        } catch (Exception exception) {
            throw new InvalidOperationException("Sign-in failed.", exception);
        }
    }
    
    public async Task SignOut() {
        await firebaseAuth.SignOutAsync();
    }
}
#endif 