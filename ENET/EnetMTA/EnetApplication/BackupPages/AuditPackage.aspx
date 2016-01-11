<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User.Master" CodeBehind="AuditPackage.aspx.cs" Inherits="EnetApplication.AuditPackage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ScrollableGrid.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvAudit.ClientID %>').Scrollable();
        }
);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="container col-md-offset-2">
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvAudit" runat="server" ShowFooter="false" Width="100%" AutoGenerateColumns="False" BorderColor="#BFBEBE" BorderStyle="None"
                        CellPadding="2" CellSpacing="3" AllowPaging="True" PageSize="30" CssClass="table table-bordered table-condensed table-hover" OnPageIndexChanging="gvAudit_PageIndexChanging" OnRowDataBound="gvAudit_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Barcode" HeaderStyle-Wrap="true" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBarcode" runat="server" Text='<%# Eval("BarcodeID") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Medicine Name" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblMedName" runat="server" Text='<%# Eval("MedicineName") %>' />
                                </ItemTemplate>

                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Expiry Date" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpiryDate" runat="server" Text='<%# Eval("ExpiryDate") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Shelf Life" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblShelflife" runat="server" Text='<%# Eval("Shelflife") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Value" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Is Temp Sensitive" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:CheckBox Enabled="false" ID="chkIsTempSensitive" runat="server" HeaderText="IsTempSensitive"
                                        Checked='<%#bool.Parse(Eval("isTempSensitive").ToString())%>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Current Location ID" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocationName") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" HeaderStyle-Wrap="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("TransitState") %>' />
                                </ItemTemplate>
                                <HeaderStyle Wrap="True"></HeaderStyle>
                            </asp:TemplateField>

                        </Columns>
                        <RowStyle HorizontalAlign="Center" />
                        <AlternatingRowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    <asp:Label ID="lblBarcodeText" runat="server" Text="Barcode" Visible ="false"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBarcode" runat="server" Visible="false" AutoPostBack="True" OnTextChanged="txtBarcode_TextChanged"></asp:TextBox>
                </div>
                
                <div class="col-md-2">
                    <asp:Button ID="btnStartAuditing" runat="server" Text="Start Auditing" OnClick="btnStartAuditing_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnLost" runat="server" Text="Set as Lost Package" OnClick="btnLost_Click" />
                </div>
            </div>
            <div class="row">
            <asp:Panel ID="pnlSuccess" Visible="False" runat="server">
                <div class="alert alert-info">
                    <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
                </div>
            </asp:Panel>
        </div>
        </div>
    </form>
</asp:Content>
