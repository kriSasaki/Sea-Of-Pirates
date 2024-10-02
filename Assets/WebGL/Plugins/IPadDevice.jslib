var HandleIO = {
    IsMobilePlatform : function()
    {
        var userAgent = navigator.userAgent;
        isMobile = (
                    (userAgent.includes("Mac") && "ontouchend" in document)
                );
        return isMobile;
    }
};
mergeInto(LibraryManager.library, HandleIO);
