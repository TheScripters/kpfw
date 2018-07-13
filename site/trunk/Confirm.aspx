<%@ Page Title="Kim Possible Fan World .:::. Confirm Account" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="kpfw.ConfirmPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="interior cf">
        <div class="inner cf">
            <h1>Confirm Account</h1>
            <asp:PlaceHolder runat="server" ID="plhSuccess">
                <p>Congratulations! You have confirmed you're human and your account is now active.</p>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plhError">
                <p>Darn! Sorry, there was a problem activating your account. Please make sure you copy the URL in your email exactly and don't include extra spaces or leave off characters on the end.</p>
                <p>If there's a problem with the website, we are probably aware of it and will work to fix it. Please try again later. If you continue to have problems please <a href="/Contact">contact us</a> immediately.</p>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" Runat="Server">
</asp:Content>