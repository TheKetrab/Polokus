import { Group } from '@bpmn-io/properties-panel';

import { findIndex } from 'min-dash';

import { mutate as arrayMove } from 'array-move';

import {
  ConditionProps,
  ScriptTaskProps,
  TimerProps,
} from './properties';

const LOW_PRIORITY = 500;

const POLOKUS_GROUPS = [
  ScriptGroup,
  ConditionGroup,
];

/**
 * Provides bpmn properties used with Polokus engine.
 *
 * @example
 * ```javascript
 * import BpmnModeler from 'bpmn-js/lib/Modeler';
 * import {
 *   BpmnPropertiesPanelModule,
 *   BpmnPropertiesProviderModule,
 *   PolokusPropertiesProviderModule
 * } from 'bpmn-js-properties-panel';
 *
 * const modeler = new BpmnModeler({
 *   container: '#canvas',
 *   propertiesPanel: {
 *     parent: '#properties'
 *   },
 *   additionalModules: [
 *     BpmnPropertiesPanelModule,
 *     BpmnPropertiesProviderModule,
 *     PolokusPropertiesProviderModule
 *   ]
 * });
 * ```
 */
export default class PolokusPropertiesProvider {

  constructor(propertiesPanel, injector) {
    propertiesPanel.registerProvider(LOW_PRIORITY, this);

    this._injector = injector;
  }

  getGroups(element) {
    return (groups) => {
      groups = groups.concat(this._getGroups(element));
      return groups;
    };
  }

  _getGroups(element) {
    const groups = POLOKUS_GROUPS.map(createGroup => createGroup(element, this._injector));

    // contract: if a group returns null, it should not be displayed at all
    return groups.filter(group => group !== null);
  }
}

PolokusPropertiesProvider.$inject = [ 'propertiesPanel', 'injector' ];


function ScriptGroup(element, injector) {
  const translate = injector.get('translate');

  const group = {
    label: translate('Script'),
    id: 'Polokus__Script',
    component: Group,
    entries: [
      ...ScriptTaskProps({ element })
    ]
  };

  if (group.entries.length) {
    return group;
  }

  return null;
}

function ConditionGroup(element, injector) {
  const translate = injector.get('translate');
  const group = {
    label: translate('Condition'),
    id: 'Polokus__Condition',
    component: Group,
    entries: [
      ...ConditionProps({ element })
    ]
  };

  if (group.entries.length) {
    return group;
  }

  return null;
}


