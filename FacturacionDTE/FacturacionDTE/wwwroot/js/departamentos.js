// departamentos.js
// Lista de departamentos y municipios de El Salvador
const departamentosData = {
    "Ahuachapán": ["Ahuachapán", "Atiquizaya", "Concepción de Ataco", "El Refugio", "Guaymango"],
    "Cabañas": ["Sensuntepeque", "Ilobasco", "Jutiapa"],
    "Chalatenango": ["Chalatenango", "La Palma", "Arcatao"],
    "Cuscatlán": ["Cojutepeque", "Suchitoto", "Cabañas"],
    "La Libertad": ["Santa Tecla", "Antiguo Cuscatlán", "Colón", "Zaragoza"],
    "La Paz": ["Zacatecoluca", "San Luis Talpa", "Olocuilta"],
    "La Unión": ["La Unión", "Meanguera del Golfo", "Anamorós"],
    "Morazán": ["San Francisco Gotera", "Arambala", "Meanguera"],
    "San Miguel": ["San Miguel", "Chinameca", "Moncagua"],
    "San Salvador": ["San Salvador", "Soyapango", "Mejicanos", "Apopa", "Ilopango"],
    "San Vicente": ["San Vicente", "Apastepeque", "San Ildefonso"],
    "Santa Ana": ["Santa Ana", "Metapán", "Chalchuapa", "Ciudad Romero"],
    "Sonsonate": ["Sonsonate", "Acajutla", "Nahuizalco"],
    "Usulután": ["Usulután", "Jiquilisco", "Berlín"]
};

// funciones globales
function getDepartamentos() {
    return Object.keys(departamentosData);
}

function getMunicipios(departamento) {
    return departamentosData[departamento] || [];
}
