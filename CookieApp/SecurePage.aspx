<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurePage.aspx.cs" Inherits="CookieApp.SecurePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="http://localhost:38206/token?client_id=clic&redirect_uri=http://localhost:17360/loader.html&response_type=token">Go to app</a>
    </div>
    </form>
        <script src="Scripts/jquery-1.9.1.min.js"></script>
</body>
</html>
