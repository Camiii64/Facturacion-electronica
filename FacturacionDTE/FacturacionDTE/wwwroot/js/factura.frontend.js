// === FACTURA ELECTRÓNICA - FARMACIA SALUD TOTAL ===
document.addEventListener('DOMContentLoaded', () => {

    // --- Departamentos y municipios ---
    const emDep = document.getElementById('emisorDepartamento');
    const emMun = document.getElementById('emisorMunicipio');
    const reDep = document.getElementById('receptorDepartamento');
    const reMun = document.getElementById('receptorMunicipio');

    const deps = getDepartamentos();
    deps.forEach(d => {
        emDep.add(new Option(d, d));
        reDep.add(new Option(d, d));
    });

    emDep.addEventListener('change', () => fillMunicipios(emDep.value, emMun));
    reDep.addEventListener('change', () => fillMunicipios(reDep.value, reMun));

    function fillMunicipios(dep, targetSelect) {
        targetSelect.innerHTML = '<option value="">Seleccione...</option>';
        if (!dep) return;
        const muns = getMunicipios(dep);
        muns.forEach(m => targetSelect.add(new Option(m, m)));
    }

    // --- Tabla de productos ---
    const tablaBody = document.querySelector('#tablaProductos tbody');
    const btnAgregar = document.getElementById('btnAgregar');

    btnAgregar.addEventListener('click', () => agregarLinea());

    function agregarLinea() {
        const row = document.createElement('tr');
        const index = tablaBody.children.length + 1;

        row.innerHTML = `
            <td>${index}</td>
            <td><input type="text" class="form-control descripcion" placeholder="Nombre del producto" required></td>
            <td><input type="number" min="1" step="1" class="form-control cantidad" value="1"></td>
            <td><input type="number" min="0" step="0.01" class="form-control precio" value="0.00"></td>
            <td>
                <select class="form-select tipoItem">
                    <option value="1">Bien</option>
                    <option value="2">Servicio</option>
                </select>
            </td>
            <td><input type="number" step="0.01" class="form-control ventaNoSuj" value="0.00"></td>
            <td><input type="number" step="0.01" class="form-control ventaExenta" value="0.00"></td>
            <td><input type="number" step="0.01" class="form-control ventaGravada" value="0.00"></td>
            <td><input type="text" class="form-control subtotal" value="0.00" readonly></td>
            <td class="text-center">
                <button type="button" class="btn btn-sm btn-danger btn-eliminar">x</button>
            </td>
        `;

        tablaBody.appendChild(row);

        // Eventos para recalcular
        row.querySelectorAll('input').forEach(input => {
            input.addEventListener('input', recalcularTotales);
        });

        // Eliminar línea
        row.querySelector('.btn-eliminar').addEventListener('click', () => {
            row.remove();
            recalcularTotales();
        });

        recalcularTotales();
    }

    // --- Cálculo automático de totales ---
    function recalcularTotales() {
        let totalGravadas = 0;
        let totalExentas = 0;
        let totalNoSuj = 0;
        let totalIVA = 0;
        let totalPagar = 0;

        document.querySelectorAll('#tablaProductos tbody tr').forEach(tr => {
            const cant = parseFloat(tr.querySelector('.cantidad').value) || 0;
            const precio = parseFloat(tr.querySelector('.precio').value) || 0;
            const vNoSuj = parseFloat(tr.querySelector('.ventaNoSuj').value) || 0;
            const vExen = parseFloat(tr.querySelector('.ventaExenta').value) || 0;
            const vGrav = parseFloat(tr.querySelector('.ventaGravada').value) || (cant * precio);

            const base = vNoSuj + vExen + vGrav;
            const ivaMonto = vGrav * 0.13;
            const subtotal = base + ivaMonto;

            tr.querySelector('.subtotal').value = subtotal.toFixed(2);

            totalNoSuj += vNoSuj;
            totalExentas += vExen;
            totalGravadas += vGrav;
            totalIVA += ivaMonto;
        });

        totalPagar = totalNoSuj + totalExentas + totalGravadas + totalIVA;

        // Actualizar campos en el resumen
        document.getElementById('totalNoSuj').value = totalNoSuj.toFixed(2);
        document.getElementById('totalExenta').value = totalExentas.toFixed(2);
        document.getElementById('totalGravada').value = totalGravadas.toFixed(2);
        document.getElementById('iva').value = totalIVA.toFixed(2);
        document.getElementById('totalPagar').value = totalPagar.toFixed(2);
    }

    // --- Actualización automática cuando cambian los campos del resumen manualmente ---
    ["totalNoSuj", "totalExenta", "totalGravada"].forEach(id => {
        const input = document.getElementById(id);
        if (input) input.addEventListener("input", calcularIVA);
    });

    // --- Cálculo de IVA independiente (por si se modifica manualmente totalGravada) ---
    function calcularIVA() {
        const totalGravada = parseFloat(document.getElementById("totalGravada").value) || 0;
        const totalExenta = parseFloat(document.getElementById("totalExenta").value) || 0;
        const totalNoSuj = parseFloat(document.getElementById("totalNoSuj").value) || 0;

        const iva = totalGravada * 0.13;
        const total = totalGravada + totalExenta + totalNoSuj + iva;

        document.getElementById("iva").value = iva.toFixed(2);
        document.getElementById("totalPagar").value = total.toFixed(2);
    }

    // --- Simulación de envío y XML ---
    const form = document.getElementById("formDTE");

    form.addEventListener("submit", e => {
        e.preventDefault();
        const xml = generarXmlSimulado();
        descargarArchivo("DTE.xml", xml);
        alert("✅ DTE generado correctamente.");
    });

    function generarXmlSimulado() {
        const doc = document.implementation.createDocument('', '', null);
        const root = doc.createElement('DTE');

        // Encabezado
        const enc = doc.createElement('Encabezado');
        enc.appendChild(tag(doc, 'TipoDocumento', form.tipoDte.value));
        enc.appendChild(tag(doc, 'NumeroControl', form.numeroControl.value));
        enc.appendChild(tag(doc, 'FechaEmision', form.fecEmi.value));
        root.appendChild(enc);

        // Receptor
        const re = doc.createElement('Receptor');
        re.appendChild(tag(doc, 'Nombre', form.nombre.value));
        re.appendChild(tag(doc, 'Documento', form.numDocumento.value));
        re.appendChild(tag(doc, 'Departamento', reDep.value));
        re.appendChild(tag(doc, 'Municipio', reMun.value));
        root.appendChild(re);

        // Detalle
        const det = doc.createElement('Detalle');
        document.querySelectorAll('#tablaProductos tbody tr').forEach((tr, idx) => {
            const it = doc.createElement('Linea');
            it.appendChild(tag(doc, 'NroLinea', idx + 1));
            it.appendChild(tag(doc, 'Descripcion', tr.querySelector('.descripcion').value));
            it.appendChild(tag(doc, 'Cantidad', tr.querySelector('.cantidad').value));
            it.appendChild(tag(doc, 'PrecioUnitario', tr.querySelector('.precio').value));
            it.appendChild(tag(doc, 'VentaGravada', tr.querySelector('.ventaGravada').value));
            it.appendChild(tag(doc, 'Subtotal', tr.querySelector('.subtotal').value));
            det.appendChild(it);
        });
        root.appendChild(det);

        // Totales
        const tot = doc.createElement('Totales');
        tot.appendChild(tag(doc, 'TotalNoSujetas', document.getElementById('totalNoSuj').value));
        tot.appendChild(tag(doc, 'TotalExentas', document.getElementById('totalExenta').value));
        tot.appendChild(tag(doc, 'TotalGravadas', document.getElementById('totalGravada').value));
        tot.appendChild(tag(doc, 'IVA', document.getElementById('iva').value));
        tot.appendChild(tag(doc, 'TotalPagar', document.getElementById('totalPagar').value));
        root.appendChild(tot);

        doc.appendChild(root);
        let xml = new XMLSerializer().serializeToString(doc);

        // Agregar referencia al archivo XSL
        const xslLink = '<?xml-stylesheet type="text/xsl" href="Style.xsl"?>\n';
        xml = '<?xml version="1.0" encoding="UTF-8"?>\n' + xslLink + xml;

        return xml;
    }

    function tag(doc, name, value) {
        const el = doc.createElement(name);
        el.textContent = value || '';
        return el;
    }

    function descargarArchivo(nombre, contenido) {
        const blob = new Blob([contenido], { type: 'application/xml' });
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = nombre;
        document.body.appendChild(a);
        a.click();
        a.remove();
        URL.revokeObjectURL(url);
    }
});
