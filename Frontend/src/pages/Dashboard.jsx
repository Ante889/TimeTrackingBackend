import React from 'react';
import { Layout, theme } from 'antd';
import Breadcrumbs from '../components/Breadcrumbs';

const { Content } = Layout;

const Dashboard = () => {
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const breadcrumbItems = [
    { label: 'Dashboard', route: '/base-image' }
  ];

  return (
    <Content style={{ margin: '0 16px' }}>
      <Breadcrumbs items={breadcrumbItems} />
      <div
        style={{
          padding: 24,
          minHeight: 360,
          background: colorBgContainer,
          borderRadius: borderRadiusLG,
        }}
      >
        <img 
          src="/Capture.PNG"
          alt="Image sql"
          style={{
            maxWidth: '100%',
            height: 'auto',
            borderRadius: borderRadiusLG,
          }} 
        />
      </div>
    </Content>
  );
};

export default Dashboard;
