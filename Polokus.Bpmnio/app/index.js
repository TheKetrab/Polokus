import $ from 'jquery';
import BpmnModeler from 'bpmn-js/lib/Modeler';
import BpmnViewer from 'bpmn-js/lib/NavigatedViewer';


import ResizeTaskModule from 'bpmn-js-task-resize/lib';

import {
  BpmnPropertiesPanelModule,
  BpmnPropertiesProviderModule
} from 'bpmn-js-properties-panel';
import PolokusPropertiesProviderModule from './provider/polokus';

import diagramXML from '../resources/newDiagram.bpmn';

import '../styles/app.less';

var container = $('#js-drop-zone');
var mode; // viewer or to edit
var bpmnModeler;
var bpmnViewer;








async function createNewDiagram() {
  await openDiagram(diagramXML);
}

async function openDiagram(xml) {

  try {

    await bpmnModeler.importXML(xml);

    container
      .removeClass('with-error')
      .addClass('with-diagram');
  } catch (err) {

    container
      .removeClass('with-diagram')
      .addClass('with-error');

    container.find('.error pre').text(err.message);

    console.error(err);
  }
}

function download(encodedData,filename) {
  var element = document.createElement('a');
  element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodedData);
  element.setAttribute('download', filename);
  document.body.appendChild(element);
  element.click();
  document.body.removeChild(element);
}

async function readFileAsync(file) {
  return new Promise((resolve, reject) => {
    let reader = new FileReader();

    reader.onload = () => {
      resolve(reader.result);
    };

    reader.onerror = reject;

    reader.readAsText(file,'UTF-8');
  })
}

// ----- ----- ----- ----- -----
//       ----- PUBLIC -----
// ----- ----- ----- ----- -----
window.api = {}

async function xml2SvgAsync(xml) {

  let modeler = bpmnModeler; //new BpmnModeler();
  await modeler.importXML(xml);
  let { svg } = await modeler.saveSvg();

  return svg;
}
window.api.xml2SvgAsync = xml2SvgAsync;

async function downloadSvgFileAsync() {

  const { svg } = await bpmnModeler.saveSVG();

  let encodedData = encodeURIComponent(svg);
  download(encodedData,"diagram.svg");

}
window.api.downloadSvgFileAsync = downloadSvgFileAsync;

async function downloadBpmnFileAsync() {

  const { xml } = await bpmnModeler.saveXML({ format: true });

  let encodedData = encodeURIComponent(xml);
  download(encodedData,"diagram.bpmn");

}
window.api.downloadBpmnFileAsync = downloadBpmnFileAsync;

function setActiveNodes(nodesIds) {

  if (bpmnViewer) {
    const canvas = bpmnViewer.get('canvas');
    nodesIds.forEach(key => canvas.addMarker(key, 'highlight'))
  }

  else if (bpmnModeler) {

    var modeling = bpmnModeler.get('modeling');
    var elementRegistry = bpmnModeler.get('elementRegistry');
  
    var elements = nodesIds.map(x => elementRegistry.get(x));
  
    modeling.setColor(elements, {
      stroke: 'green',
      fill: 'green'
    });
      
  }

  else {
    throw 'Unknown mode.';
  }

}
window.api.setActiveNodes = setActiveNodes;

function setInactiveNodes(nodesIds) {

  debugger;
  if (bpmnViewer) {
    const canvas = bpmnViewer.get('canvas');
    nodesIds.forEach(key => canvas.removeMarker(key, 'highlight'))
  }

  else if (bpmnModeler) {

    var modeling = bpmnObj.get('modeling');
    var elementRegistry = bpmnObj.get('elementRegistry');

    var elements = nodesIds.map(x => elementRegistry.get(x));

    modeling.setColor(elements, {
      stroke: 'black',
      fill: 'white'
    });
  }

  else {
    throw 'Unknown mode.';
  }
}
window.api.setInactiveNodes = setInactiveNodes;

function updateColoursForNodes(activeNodesIds,inactiveNodesIds) {
  setInactiveNodes(inactiveNodesIds);
  setActiveNodes(activeNodesIds);
}

window.api.updateColoursForNodes = updateColoursForNodes;

async function loadBpmnAsync(xml) {

  await bpmnModeler.importXML(xml);
}
window.api.loadBpmnAsync = loadBpmnAsync;


async function getSvgAsync() {
  const {svg} = await bpmnModeler.saveSvg();
  return svg;
}
window.api.getSvgAsync = getSvgAsync;

async function openInViewerAsync(bpmnXML) {

  debugger;
  try {
    await bpmnViewer.importXML(bpmnXML);
    var canvas = bpmnViewer.get('canvas');
    canvas.zoom('fit-viewport');

    container
    .removeClass('with-error')
    .addClass('with-diagram');
  } catch (err) {

  container
    .removeClass('with-diagram')
    .addClass('with-error');

    container.find('.error pre').text(err.message);

    console.error(err);
  }

}
window.api.openInViewerAsync = openInViewerAsync;

// ----- ----- ----- ----- -----
//       ----- MAIN -----
// ----- ----- ----- ----- -----

function initialize(mode) {

    // ----- CREATE MODELER ----
    if (mode == 'modeler') {
  
    bpmnModeler = new BpmnModeler({
      container: '#js-canvas',
      propertiesPanel: {
        parent: '#js-properties-panel'
      },
      additionalModules: [
        BpmnPropertiesPanelModule,
        BpmnPropertiesProviderModule,
        PolokusPropertiesProviderModule,
        ResizeTaskModule,
      ],
      moddleExtensions: {
    
      },
      taskResizingEnabled: true,
      eventResizingEnabled: false,
      // gate resizing -> not supported by this lib
    });

    $('#js-download-diagram').on('click', function(e) {
      downloadBpmnFileAsync();
    });
    $('#js-download-svg').on('click', function(e) {
      downloadSvgFileAsync();
    });
    $('#js-download-png').on('click', async function(e) {
      var svg = await window.api.getSvgAsync();
      if (callbackObj) {
        callbackObj.downloadPng(svg);
      }    
    });
    $('#js-open-file').on('change', async function() {
  
      var file = this.files[0];
      var text = await readFileAsync(file);
      await window.api.loadBpmnAsync(text);
    });
  

    createNewDiagram();

  }
  
  // ----- CREATE VIEWER -----
  else {

    bpmnViewer = new BpmnViewer({ 
      container: '#js-canvas'
    });

    $('.buttons-panel').css('display','none');
    openInViewerAsync(diagramXML);
  
  }

}

$(function() {

  const params = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
  });
  
  if (params.mode) {
    mode = params.mode;
  } else {
    mode = "modeler"
  }

  initialize(mode);

});
// ----- ----- ----- ----- -----



