import { initListadoEmpleados } from './listadoEmpleados.js';
import { initFormularioIngreso } from './formularioIngreso.js';
import { initReporteEmpleados } from './reporteEmpleados.js';

document.addEventListener('DOMContentLoaded', () => {
    const navListado = document.getElementById('nav-listado');
    const navFormulario = document.getElementById('nav-formulario');
    const navReporte = document.getElementById('nav-reporte');

    const sectionListado = document.getElementById('listado-empleados');
    const sectionFormulario = document.getElementById('formulario-ingreso');
    const sectionReporte = document.getElementById('reporte-empleados');

    function showSection(section) {
        [sectionListado, sectionFormulario, sectionReporte].forEach(s => s.style.display = 'none');
        section.style.display = 'block';
    }

    navListado.addEventListener('click', (e) => {
        e.preventDefault();
        showSection(sectionListado);
        initListadoEmpleados();
    });

    navFormulario.addEventListener('click', (e) => {
        e.preventDefault();
        showSection(sectionFormulario);
        initFormularioIngreso();
    });

    navReporte.addEventListener('click', (e) => {
        e.preventDefault();
        showSection(sectionReporte);
        initReporteEmpleados();
    });

    // Mostrar el listado de empleados por defecto
    showSection(sectionListado);
    initListadoEmpleados();
});