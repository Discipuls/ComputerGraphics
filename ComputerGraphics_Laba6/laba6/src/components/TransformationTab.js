import React, { useEffect, useState } from 'react';
import * as THREE from 'three';

const TransformationTab = () => {
  const [vertices, setVertices] = useState([
    [0, 0, 0], [0, 1, 0], [1, 1, 0], [1, 0, 0], // Bottom square
    [0, 0, 1], [0, 1, 1], [1, 1, 1], [1, 0, 1], // Top square
    [1, 0, 0], [1, 0, 1], // Vertical part
  ]);
  const [transformedVertices, setTransformedVertices] = useState(vertices);
  const [transformationMatrix, setTransformationMatrix] = useState([[1, 0, 0], [0, 1, 0], [0, 0, 1]]);

  const handleTransformation = (transformation) => {
    const newVertices = applyTransformation(vertices, transformation);
    const matrix = getTransformationMatrix(transformation);
    setTransformedVertices(newVertices);
    setTransformationMatrix(matrix);
  };

  useEffect(() => {
    initThreeJS();
  }, []);

  const initThreeJS = () => {
    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
    const renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    document.body.appendChild(renderer.domElement);

    const geometry = new THREE.Geometry();
    vertices.forEach(([x, y, z]) => {
      geometry.vertices.push(new THREE.Vector3(x, y, z));
    });

    const material = new THREE.PointsMaterial({ color: 0x00ff00, size: 0.1 });
    const points = new THREE.Points(geometry, material);
    scene.add(points);

    camera.position.z = 5;

    const animate = function () {
      requestAnimationFrame(animate);
      renderer.render(scene, camera);
    };

    animate();
  };

  const applyTransformation = (vertices, transformation) => {
    const { type, params } = transformation;
    let transformedVertices = vertices.map(([x, y, z]) => [x, y, z]);

    if (type === 'scale') {
      const [sx, sy, sz] = params;
      transformedVertices = transformedVertices.map(([x, y, z]) => [x * sx, y * sy, z * sz]);
    } else if (type === 'translate') {
      const [dx, dy, dz] = params;
      transformedVertices = transformedVertices.map(([x, y, z]) => [x + dx, y + dy, z + dz]);
    } else if (type === 'rotate') {
      const [axisX, axisY, axisZ, angle] = params;
      const radians = (angle * Math.PI) / 180;
      transformedVertices = transformedVertices.map(([x, y, z]) => {
        const cosA = Math.cos(radians);
        const sinA = Math.sin(radians);
        const dot = x * axisX + y * axisY + z * axisZ;
        const cross = [
          y * axisZ - z * axisY,
          z * axisX - x * axisZ,
          x * axisY - y * axisX,
        ];
        return [
          cosA * x + (1 - cosA) * dot * axisX + sinA * cross[0],
          cosA * y + (1 - cosA) * dot * axisY + sinA * cross[1],
          cosA * z + (1 - cosA) * dot * axisZ + sinA * cross[2],
        ];
      });
    }

    return transformedVertices;
  };

  const getTransformationMatrix = (transformation) => {
    const { type, params } = transformation;
    let matrix = [[1, 0, 0], [0, 1, 0], [0, 0, 1]];

    if (type === 'scale') {
      const [sx, sy, sz] = params;
      matrix = [[sx, 0, 0], [0, sy, 0], [0, 0, sz]];
    } else if (type === 'translate') {
      const [dx, dy, dz] = params;
      matrix = [[1, 0, 0], [0, 1, 0], [dx, dy, dz]];
    } else if (type === 'rotate') {
      const [axisX, axisY, axisZ, angle] = params;
      const radians = (angle * Math.PI) / 180;
      const cosA = Math.cos(radians);
      const sinA = Math.sin(radians);
      matrix = [
        [cosA + (1 - cosA) * axisX ** 2, (1 - cosA) * axisX * axisY - sinA * axisZ, (1 - cosA) * axisX * axisZ + sinA * axisY],
        [(1 - cosA) * axisY * axisX + sinA * axisZ, cosA + (1 - cosA) * axisY ** 2, (1 - cosA) * axisY * axisZ - sinA * axisX],
        [(1 - cosA) * axisZ * axisX - sinA * axisY, (1 - cosA) * axisZ * axisY + sinA * axisX, cosA + (1 - cosA) * axisZ ** 2],
      ];
    }
    return matrix;
  };

  return (
    <div>
      <h2>3D Transformations</h2>
      <div>
        <button onClick={() => handleTransformation({ type: 'scale', params: [2, 2, 2] })}>Scale</button>
        <button onClick={() => handleTransformation({ type: 'translate', params: [1, 1, 0] })}>Translate</button>
        <button onClick={() => handleTransformation({ type: 'rotate', params: [1, 0, 0, 90] })}>Rotate</button>
      </div>
      <div>
        <h3>Transformed Vertices:</h3>
        <pre>{JSON.stringify(transformedVertices, null, 2)}</pre>
      </div>
      <div>
        <h3>Transformation Matrix:</h3>
        <pre>{JSON.stringify(transformationMatrix, null, 2)}</pre>
      </div>
    </div>
  );
};

export default TransformationTab;
