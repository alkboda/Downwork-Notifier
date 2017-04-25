# Downwork-Notifier
That’s WPF application to work with Upwork API. For now it does fast nothing except of receiving job offers. There is possibility to keep several pages with different filters, rows with jobs could be expanded for more details and have buttons with url to appropriate job web page. API has much more possibilities, which I hopefully will add sometime.
Main reason to put solution onto github is .NetStandard1.1 library(ApiLibrary) which interact with API and is written in C# – I didn’t find any implementations of similar libraries for .Net, so I hope it’ll be helpful for someone.

I tried to make application more comfortable (at least because I use it by myself), so application minimizes to tray instead of closing (actually that’s little unexpected) – to close the app choose “Close” from context menu of tray icon. There is EF7 + SQL CE used to store pages and filters, and are implemented popups for newly found jobs (on double-click on popup text you will open appropriate tab in application), popups could be disabled, otherwise it’s possible to set how long in seconds popup will be showed.

If you have your API keys and want to test application, then put your keys into "Downwork Notifier/API/ApiKey.cs". Then on start press "Login" with empty fields, then appropriate url will be opened with your broswer, where you will grant access to your profile and will receive verifier code, which you should input to appeared dialog within the app.

Unfortunately I can’t share my API keys, so I can’t put binary compiled version right away, because keys are easily extracted in this case. There is a way to receive API key by yourself and use it. Otherwise wait some time while I’m adding new separate library for requesting API under some license, which deny reverse engineering, because I don’t want to be responsible for key loss. If you know some other way to more securely share binaries, then let me know.

I'm pretty new to all this open-source stuff, so don't blame me too much, please, and I will be thankfull for all useful advices. I’m open to any offers about ways to improve current implementation, so don’t hesitate to share your minds.
