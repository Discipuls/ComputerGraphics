import React, { useState } from 'react';
import { applyProjection } from '../utils/transformations';

const ProjectionTab = () => {
  const [vertices, setVertices] = useState([
    [0, 0, 0], [0, 1, 0], [1, 1, 0], [1, 0, 0], // Bottom square
    [0, 0, 1], [0, 1, 1], [1, 1, 1], [1, 0, 1], // Top square
    [1, 0, 0], [1, 0, 1], // Vertical part
  ]);
  const [projectionVertices, setProjectionVertices] = useState(vertices);

  const handleProjection = (plane) => {
    const newVertices = applyProjection(vertices, plane);
    setProjectionVertices(newVertices);
  };

  return (
    <div>
      <h2>Orthographic Projections</h2>
      <div>
        <button onClick={() => handleProjection('xy')}>Oxy Projection</button>
        <button onClick={() => handleProjection('yz')}>Oyz Projection</button>
        <button onClick={() => handleProjection('xz')}>Oxz Projection</button>
      </div>
      <div>
        <h3>Projected Vertices:</h3>
        <pre>{JSON.stringify(projectionVertices, null, 2)}</pre>
      </div>
    </div>
  );
};

export default ProjectionTab;
