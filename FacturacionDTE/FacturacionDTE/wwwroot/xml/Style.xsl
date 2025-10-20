<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="html" encoding="UTF-8" indent="yes"/>

	<xsl:template match="/">
		<html>
			<head>
				<title>Factura Electrónica DTE</title>
				<style>
					body {
					font-family: 'Segoe UI', Arial, sans-serif;
					margin: 40px;
					background-color: #f6f6f6;
					}
					.factura {
					background: #fff;
					padding: 30px;
					border-radius: 10px;
					box-shadow: 0 0 10px rgba(0,0,0,0.1);
					max-width: 800px;
					margin: auto;
					}
					h1 {
					text-align: center;
					color: #2c3e50;
					border-bottom: 2px solid #3498db;
					padding-bottom: 10px;
					}
					table {
					width: 100%;
					border-collapse: collapse;
					margin-top: 15px;
					}
					th, td {
					border: 1px solid #ddd;
					padding: 8px;
					text-align: left;
					}
					th {
					background-color: #3498db;
					color: white;
					}
					.totales td {
					font-weight: bold;
					background-color: #ecf0f1;
					}
					.seccion {
					margin-top: 20px;
					}
					.seccion h2 {
					font-size: 1.2em;
					color: #34495e;
					border-bottom: 1px solid #ccc;
					padding-bottom: 5px;
					}
				</style>
			</head>
			<body>
				<div class="factura">
					<h1>Documento Tributario Electrónico (DTE)</h1>

					<div class="seccion">
						<h2>Encabezado</h2>
						<table>
							<tr>
								<th>Tipo Documento</th>
								<td>
									<xsl:value-of select="DTE/Encabezado/TipoDocumento"/>
								</td>
							</tr>
							<tr>
								<th>Número Control</th>
								<td>
									<xsl:value-of select="DTE/Encabezado/NumeroControl"/>
								</td>
							</tr>
							<tr>
								<th>Fecha Emisión</th>
								<td>
									<xsl:value-of select="DTE/Encabezado/FechaEmision"/>
								</td>
							</tr>
						</table>
					</div>

					<div class="seccion">
						<h2>Receptor</h2>
						<table>
							<tr>
								<th>Nombre</th>
								<td>
									<xsl:value-of select="DTE/Receptor/Nombre"/>
								</td>
							</tr>
							<tr>
								<th>Documento</th>
								<td>
									<xsl:value-of select="DTE/Receptor/Documento"/>
								</td>
							</tr>
							<tr>
								<th>Departamento</th>
								<td>
									<xsl:value-of select="DTE/Receptor/Departamento"/>
								</td>
							</tr>
							<tr>
								<th>Municipio</th>
								<td>
									<xsl:value-of select="DTE/Receptor/Municipio"/>
								</td>
							</tr>
						</table>
					</div>

					<div class="seccion">
						<h2>Detalle</h2>
						<table>
							<tr>
								<th>N°</th>
								<th>Descripción</th>
								<th>Cantidad</th>
								<th>Precio Unitario</th>
								<th>Venta Gravada</th>
								<th>Subtotal</th>
							</tr>
							<xsl:for-each select="DTE/Detalle/Linea">
								<tr>
									<td>
										<xsl:value-of select="NroLinea"/>
									</td>
									<td>
										<xsl:value-of select="Descripcion"/>
									</td>
									<td>
										<xsl:value-of select="Cantidad"/>
									</td>
									<td>
										<xsl:value-of select="PrecioUnitario"/>
									</td>
									<td>
										<xsl:value-of select="VentaGravada"/>
									</td>
									<td>
										<xsl:value-of select="Subtotal"/>
									</td>
								</tr>
							</xsl:for-each>
						</table>
					</div>

					<div class="seccion">
						<h2>Totales</h2>
						<table class="totales">
							<tr>
								<td>Total No Sujetas</td>
								<td>
									<xsl:value-of select="DTE/Totales/TotalNoSujetas"/>
								</td>
							</tr>
							<tr>
								<td>Total Exentas</td>
								<td>
									<xsl:value-of select="DTE/Totales/TotalExentas"/>
								</td>
							</tr>
							<tr>
								<td>Total Gravadas</td>
								<td>
									<xsl:value-of select="DTE/Totales/TotalGravadas"/>
								</td>
							</tr>
							<tr>
								<td>IVA</td>
								<td>
									<xsl:value-of select="DTE/Totales/IVA"/>
								</td>
							</tr>
							<tr>
								<td>Total a Pagar</td>
								<td>
									<xsl:value-of select="DTE/Totales/TotalPagar"/>
								</td>
							</tr>
						</table>
					</div>

					<div style="text-align:center; margin-top:30px; color:#7f8c8d;">
						<p>Gracias por su compra.</p>
						<p>Documento generado electrónicamente - No requiere firma autógrafa.</p>
					</div>
				</div>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
