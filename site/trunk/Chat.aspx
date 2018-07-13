<%@ Page Title="Chat" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Chat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="interior cf">
        <div class="inner">
            <h1>Chat</h1>
            <h2 runat="server" visible="false">Discord</h2>
            <p>Join our Discord server and join in the discussion about Kim Possible and anything else! It's family friendly so anyone can join.</p>
            <p>Discord is a new and upcoming chat server platform that operates similar to Skype, TeamSpeak, etc. It's easy to use and has a web interface as well.</p>
            <p>Discord Server: <a href="http://discord.gg/EcNzjcG">http://discord.gg/EcNzjcG</a></p>

            <iframe src="https://discordapp.com/widget?id=390008227946823681&theme=dark" width="350" height="500" allowtransparency="true" frameborder="0"></iframe>

            <asp:PlaceHolder runat="server" Visible="false">
                <h2>IRC</h2>
                <ul>
                    <li><b>Host:</b> <a href="irc://irc.blitzed.org">irc.blitzed.org</a></li>
                    <li><b>Channel:</b> #kpfw</li>
                </ul>
                <p>Launch web client: <a href="http://cgiirc.blitzed.org/?adv=1&chan=%23kpfw" target="_blank">http://cgiirc.blitzed.org/?adv=1&chan=%23kpfw</a></p>
                <p>Registration is not required and there is no password required to gain access to the chat area. If you're new to IRC, you may refer to <a href="http://web.archive.org/web/20120717020215/http://wiki.blitzed.org/New_Users" target="_blank">this</a> if you wish. You may use any of the following 3rd party clients to access the chat area:</p>
                <ul>
                    <li><a href="http://www.mirc.com">mIRC</a> - {Free for 30 days $20 after that} Windows</li>
                    <li><a href="https://www.trillian.im/download/">Trillian</a> - {Trillian Basic is free w/ IRC support} Windows (recommended)</li>
                    <li><a href="https://www.mutterirc.com/">Mutter</a> - {Free} iOS (recommended)</li>
                    <li><a href="http://www.androirc.com/">AndroIRC</a> - {Free /w ads or $2.90} Android</li>
                </ul>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
