<%@ Page Title="Kim Possible Fan World .:::. Contact Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="interior cf">
        <asp:PlaceHolder runat="server" ID="plhContact">

            <div class="inner cf">
                <div class="contact-content">
                    <h1>Contact Us</h1>
                    <p>Yo! Question? Comment? Something we can do better? Drop us a line and let us know!</p>
                    <p>Just as a friendly reminder, we do not represent The Walt Disney Company and we have no influence there. We can't help you with getting <i>Kim Possible</i> in your country, for example. If you want to get a hold of Disney, we recommend you check out <a href="http://www.savedisneyshows.org/help.php" target="_blank">Save Disney Shows</a>. They have all the information you need!</p>
                </div>
                <div class="contact-form">
                    <asp:ValidationSummary runat="server" CssClass="error" DisplayMode="BulletList" ShowSummary="true" />
                    <div class="item">
                        <label>
                            Name: <span class="req">Required
                            <asp:RequiredFieldValidator runat="server" CssClass="error" ControlToValidate="txtName" Text="*" ErrorMessage="Name is required" /></span></label>
                        <asp:TextBox runat="server" ID="txtName" />
                    </div>
                    <div class="item">
                        <label>
                            Email: <span class="req">Required
                            <asp:RequiredFieldValidator runat="server" CssClass="error" ControlToValidate="txtName" Text="*" ErrorMessage="Email is required" /></span></label>
                        <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" />
                    </div>
                    <div class="item">
                        <label>
                            Subject: <span class="req">Required
                            <asp:RequiredFieldValidator runat="server" CssClass="error" ControlToValidate="txtName" Text="*" ErrorMessage="Subject is required" /></span></label>
                        <asp:TextBox runat="server" ID="txtSubject" />
                    </div>
                    <div class="item">
                        <label>
                            Message: <span class="req">Required
                            <asp:RequiredFieldValidator runat="server" CssClass="error" ControlToValidate="txtName" Text="*" ErrorMessage="Message is required" /></span></label>
                        <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" />
                    </div>
                    <div class="item r" style="margin-top:10px;">
                        <div class="g-recaptcha" data-sitekey="6LcgoVQUAAAAAAERY2Q5exPExMh2TYZoSKfGnNHt"></div>
                    </div>
                    <div class="actions r">
                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
                        <p style="display: none;">
                            <asp:TextBox runat="server" ID="ContactReason" />
                        </p>
                    </div>
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plhSuccess" Visible="false">
            <div class="inner cf">
                <p>Thank you for contacting us! We will try to get back to you as soon as possible!</p>
            </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphScripts" runat="server">
</asp:Content>