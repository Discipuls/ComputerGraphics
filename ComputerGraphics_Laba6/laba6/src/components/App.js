import React, { useState } from 'react';
import TransformationTab from './TransformationTab';
import ProjectionTab from './ProjectionTab';

const App = () => {
  const [activeTab, setActiveTab] = useState('transformation');

  return (
    <div className="app">
      <div className="tabs">
        <button onClick={() => setActiveTab('transformation')}>3D Transformations</button>
        <button onClick={() => setActiveTab('projection')}>Projections</button>
      </div>
      {activeTab === 'transformation' ? <TransformationTab /> : <ProjectionTab />}
    </div>
  );
};

export default App;
