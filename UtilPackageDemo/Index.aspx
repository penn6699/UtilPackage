<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UtilPackageDemo.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Index</title>
    <style>
        div {
            margin:10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>SystemVirtualDirectory: <%=UtilPackage.SystemVirtualDirectory %></div>
        <%
            string md5Str = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            %>
        <div>md5Str: <%=md5Str %></div>
        <div>Md5-32: <%=UtilPackage.Md5(md5Str) %></div>
        <div>Md5-16: <%=UtilPackage.Md5To16Bit(md5Str) %></div>
    </form>
</body>
</html>
