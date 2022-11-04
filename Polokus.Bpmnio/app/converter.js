
export async function getJpg(bpmnModeler) {
    
    debugger;
    const { svg } = await bpmnModeler.saveSVG();
    return svg;
    // svg2jpg(svg);
    // debugger;


}


function svg2jpg(svgString) {
    //var svgString = new XMLSerializer().serializeToString(svgElement);
    var decoded = decodeURIComponent(encodeURIComponent(svgString)); // remove unsupported characters
    var base64 = btoa(decoded);
    return base64;
}

