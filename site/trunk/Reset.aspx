<%@ Page Title="Kim Possible Fan World .:::. Reset Password" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reset.aspx.cs" Inherits="kpfw.ResetPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="interior cf">
        <div class="inner cf">
            <h1>Reset Password</h1>
            <asp:PlaceHolder runat="server" ID="plhResetPassword">
                <p>Enter your new password below to change it. This will take effect immediately and you will be able to log in.</p>
                <asp:ValidationSummary runat="server" CssClass="error" ValidationGroup="ResetForm" />
                <div class="item cf">
                    <div class="halfi">
                        <div class="item">
                            <label>New Password <span class="req">Required
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="Dynamic" CssClass="error" Text="*" ErrorMessage="New Password is required" ValidationGroup="ResetForm" /></span></label>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" />
                        </div>
                        <div class="item">
                            <label>Confirm Password <span class="req">Required
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" CssClass="error" Text="*" ErrorMessage="You must confirm your new password" ValidationGroup="ResetForm" />
                                <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="Dynamic" CssClass="error" Text="*" ErrorMessage="Passwords must match" ValidationGroup="ResetForm" /></span></label>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtConfirmPassword" />
                        </div>
                    </div>
                    <div class="halfi">
                    </div>
                </div>
                <div class="actions">
                    <asp:Button runat="server" Text="Change Password" ValidationGroup="ResetForm" ID="btnResetPassword" OnClick="btnResetPassword_Click" />
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plhSuccess" Visible="false">
                <p>Congratulations! Your password has been changed. You will now be able to <a href="#loginModalPopup" class="sign-in">log in</a>.</p>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plhError" Visible="false">
                <p>Darn! Sorry, there was a problem resetting your password. Please make sure you copy the URL in your email exactly and don't include extra spaces or leave off characters on the end. It's also possible the link has expired or has been used previously. If you are sure everything's correct and you continue to have problems please <a href="/Contact">contact us</a> immediately.</p>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScripts" Runat="Server">
</asp:Content>