import React from 'react';
import { Breadcrumb } from 'antd';
import { Link } from 'react-router-dom';

const Breadcrumbs = ({ items }) => {
  return (
    <Breadcrumb style={{ margin: '16px 0' }}>
      {items.map((item, index) => (
        <Breadcrumb.Item key={index}>
          <Link to={item.route}>{item.label}</Link>
        </Breadcrumb.Item>
      ))}
    </Breadcrumb>
  );
};

export default Breadcrumbs;
