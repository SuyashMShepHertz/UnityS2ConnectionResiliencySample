#How to use Connection Resiliency
![Screenshot](https://raw.githubusercontent.com/SuyashMShepHertz/UnityS2ConnectionResiliencySample/master/Screenshot/screenshot.png)
To recover connection make sure you have set the recovery allowance by calling the setRecoveryAllowance API.

```C#
    WarpClient.setRecoveryAllowance (60);
```

In the above code snippet, the 60 represents the time allowed to recover the connection. It's in seconds. If you did not recover the connection within the defined time, you will be considered fully disconnected.

When the connection breaks, you will receive the onConnectDone Listener with `WarpResponseResultCode.CONNECTION_ERROR_RECOVERABLE` result code if recovery allowance was set. 

To recover the connection simply call the recoverConnection() method. If you called the recover connection API before the time defined in the recovery allowance your connection will be successfully recovered with onConnectDone having `WarpResponseResultCode.SUCCESS_RECOVERED` result code. If the recover connection request was made too late, your request will fail with onConnectDone having `WarpResponseResultCode.AUTH_ERROR` result code.

```C#
    WarpClient.GetInstance().RecoverConnection();
```

When recovery allowance is set and your connection breaks all users receive onUserPaused() notification listener. When you successfully recover they will receive onUserResumed() notification listener.
