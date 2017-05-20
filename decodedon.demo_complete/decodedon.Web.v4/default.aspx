<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="decodedon.Web._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:LoginName runat="server" FormatString="@ {0}" />
        <asp:TextBox runat="server" ID="TootText" TextMode="MultiLine" MaxLength="500" Rows="6" />
        <asp:Button runat="server" ID="TootButton" OnClick="TootButton_Click" Text="トゥート!" />
    </div>
    <div>
        <div>ローカル</div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView runat="server" ID="LocalTimeline" AutoGenerateColumns="false" ShowHeader="false">
                    <Columns>
                        <asp:BoundField DataField="Name" HtmlEncode="true"/>
                        <asp:BoundField DataField="CreateAt" />
                        <asp:BoundField DataField="Text" HtmlEncode="true" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div>
        <div>連合</div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView runat="server" ID="FederatedTimeline" AutoGenerateColumns="false" ShowHeader="false">
                    <Columns>
                        <asp:BoundField DataField="Name" HtmlEncode="true"/>
                        <asp:BoundField DataField="CreateAt" />
                        <asp:BoundField DataField="Text" HtmlEncode="true" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:Timer runat="server" ID="Timer" Interval="3000" />
</asp:Content>
